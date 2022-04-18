using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DigitalSkynet.DotnetCore.DataAccess.Enums;
using DigitalSkynet.DotnetCore.DataStructures.Exceptions.Api;
using DigitalSkynet.DotnetCore.DataStructures.Interfaces;
using DigitalSkynet.DotnetCore.DataStructures.Models.Paging;
using Microsoft.EntityFrameworkCore;

namespace DigitalSkynet.DotnetCore.DataAccess.Repository;

public class GenericRepository<TDbContext, TEntity, TKey> : BaseRepository<TDbContext, TEntity>, IGenericRepository<TEntity, TKey>
    where TDbContext : DbContext
    where TEntity : class, IHasKey<TKey>
    where TKey : struct
{

    public GenericRepository(TDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    { }

    #region Read
    /// <summary>
    /// Gets the entity by id 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public virtual Task<TEntity> GetByIdAsync(TKey id, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default)
        => FindFirstAsync(x => x.Id.Equals(id), modes, ct);

    /// <summary>
    /// Gets the list of entities by given list of ids
    /// </summary>
    /// <param name="keys"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> GetByIdsAsync(IEnumerable<TKey> keys, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default)
    {
        var result = await FilterAsync(x => keys.Contains(x.Id), modes, ct);
        var notFoundKeys = keys.Where(x => !result.Any(e => e.Id.Equals(x))).Select(x => x.ToString()).ToList();
        if (notFoundKeys.Any())
            throw new ApiNotFoundException(typeof(TEntity).Name, notFoundKeys);
        return result;
    }

    /// <summary>
    /// Gets the projection of single entity by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TProjection"></typeparam>
    /// <returns></returns>
    public virtual Task<TProjection> GetProjectionByIdAsync<TProjection>(TKey id, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default)
        => Filter(x => x.Id.Equals(id), modes)
                .ProjectTo<TProjection>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(ct);

    /// <summary>
    /// Gets the projection of paged result filtered by predicate
    /// </summary>
    /// <param name="pagingModel"></param>
    /// <param name="predicate"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TProjection"></typeparam>
    /// <returns></returns>
    public virtual Task<Paged<TProjection>> GetPagedProjectionAsync<TProjection>(PagingModel pagingModel,
        Expression<Func<TProjection, bool>> predicate,
        FetchModes modes = FetchModes.NoTracking,
        CancellationToken ct = default)
        => ProjectPagedToAsync(predicate, pagingModel.PageNumber, pagingModel.PageSize, pagingModel.Sort, modes,
            ct);

    /// <summary>
    /// Gets the projection of paged result filtered by predicate
    /// </summary>
    /// <param name="pagingModel"></param>
    /// <param name="predicate"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TProjection"></typeparam>
    /// <returns></returns>
    public virtual Task<Paged<TProjection>> GetPagedProjectionByEntityAsync<TProjection>(PagingModel pagingModel,
        Expression<Func<TEntity, bool>> predicate,
        FetchModes modes = FetchModes.NoTracking,
        CancellationToken ct = default)
        => ProjectPagedByEntityToAsync<TProjection>(predicate, pagingModel.PageNumber, pagingModel.PageSize, pagingModel.Sort, modes,
            ct);

    /// <summary>
    /// Gets the projection of paged result filtered by predicate
    /// </summary>
    /// <param name="pagingModel"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TProjection"></typeparam>
    /// <returns></returns>
    public virtual Task<Paged<TProjection>> GetPagedProjectionByEntityAsync<TProjection>(PagingModel<TEntity> pagingModel,
        FetchModes modes = FetchModes.NoTracking,
        CancellationToken ct = default)
        => GetPagedProjectionByEntityAsync<TProjection>(pagingModel, pagingModel.GetPredicate(), modes, ct);

    /// <summary>
    /// Gets the projection of paged result filtered by predicate
    /// </summary>
    /// <param name="pagingModel"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TProjection"></typeparam>
    /// <returns></returns>
    public virtual Task<Paged<TProjection>> GetPagedProjectionAsync<TProjection>(PagingModel<TProjection> pagingModel,
        FetchModes modes = FetchModes.NoTracking,
        CancellationToken ct = default)
        => GetPagedProjectionAsync<TProjection>(pagingModel, pagingModel.GetPredicate(), modes, ct);

    #endregion

    #region Update

    /// <summary>
    /// Updates the entity
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TModel"></typeparam>
    /// <returns></returns>
    public virtual async Task UpdateAsync<TModel>(TKey id, TModel model, FetchModes modes = FetchModes.Tracking, CancellationToken ct = default)
    {
        var finalMode = modes | FetchModes.Tracking;
        var entity = await GetByIdAsync(id, modes);
        _mapper.Map(model, entity);
    }

    /// <summary>
    /// Updates the dictionary of entities
    /// </summary>
    /// <param name="models"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TModel"></typeparam>
    /// <returns></returns>
    public virtual async Task UpdateAsync<TModel>(Dictionary<TKey, TModel> models, FetchModes modes = FetchModes.Tracking, CancellationToken ct = default)
    {
        var finalMode = modes | FetchModes.Tracking;
        var entities = await GetByIdsAsync(models.Keys, modes: finalMode, ct: ct);
        foreach (var entity in entities)
            _mapper.Map(models[entity.Id], entity);
    }

    #endregion  

    #region Delete

    /// <summary>
    /// Hardly deletes the entity by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public virtual async Task DeleteHardAsync(TKey id, FetchModes modes = FetchModes.Tracking, CancellationToken ct = default)
    {
        var finalMode = modes | FetchModes.Tracking;
        var entity = await GetByIdAsync(id, finalMode, ct);
        if (entity == null)
            throw new ApiNotFoundException(typeof(TEntity).Name, id.ToString());
        DeleteHard(entity);
    }

    #endregion

    #region Check existance

    /// <summary>
    /// Checks if the entity with a given id exists
    /// </summary>
    /// <param name="id"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public virtual Task<bool> ExistsAsync(TKey id, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default)
    {
        return ExistsAsync(x => x.Id.Equals(id), modes, ct);
    }

    #endregion
}
