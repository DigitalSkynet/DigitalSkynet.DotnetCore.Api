using System.Linq.Expressions;
using DigitalSkynet.DotnetCore.DataAccess.Enums;
using DigitalSkynet.DotnetCore.DataStructures.Interfaces;
using DigitalSkynet.DotnetCore.DataStructures.Models.Paging;

namespace DigitalSkynet.DotnetCore.DataAccess.Repository;

public interface IGenericRepository<TEntity, TKey> : IBaseRepository<TEntity>
    where TEntity : class, IHasKey<TKey>
    where TKey : struct
{
    /// <summary>
    /// Gets the entity by id
    /// </summary>
    /// <param name="id">id of the entity</param>
    /// <param name="noTracking"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<TEntity> GetByIdAsync(TKey id, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default);

    /// <summary>
    /// Gets the list of the entities by given ids
    /// </summary>
    /// <param name="keys"></param>
    /// <param name="noTracking"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<List<TEntity>> GetByIdsAsync(IEnumerable<TKey> keys, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default);

    /// <summary>
    /// Gets the data by id and projects it to a given type
    /// </summary>
    /// <param name="id"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TProjection"></typeparam>
    /// <returns></returns>
    Task<TProjection> GetProjectionByIdAsync<TProjection>(TKey id, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default);

    /// <summary>
    /// Gets a paged collection based on the request model and a custom predicate
    /// </summary>
    /// <param name="pagingModel"></param>
    /// <param name="predicate"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TProjection"></typeparam>
    /// <returns></returns>
    Task<Paged<TProjection>> GetPagedProjectionAsync<TProjection>(PagingModel pagingModel,
        Expression<Func<TProjection, bool>> predicate,
        FetchModes modes = FetchModes.NoTracking,
        CancellationToken ct = default);

    /// <summary>
    /// Gets a paged collection based on the predicate defined in the model itself.
    /// </summary>
    /// <param name="pagingModel"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TProjection"></typeparam>
    /// <returns></returns>
    Task<Paged<TProjection>> GetPagedProjectionAsync<TProjection>(PagingModel<TProjection> pagingModel,
        FetchModes modes = FetchModes.NoTracking,
        CancellationToken ct = default);

    /// <summary>
    /// Gets a paged collection based on the request model and a custom predicate
    /// </summary>
    /// <param name="pagingModel"></param>
    /// <param name="predicate"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TProjection"></typeparam>
    /// <returns></returns>
    Task<Paged<TProjection>> GetPagedProjectionByEntityAsync<TProjection>(PagingModel pagingModel,
        Expression<Func<TEntity, bool>> predicate,
        FetchModes modes = FetchModes.NoTracking,
        CancellationToken ct = default);

    /// <summary>
    /// Gets a paged collection based on the predicate defined in the model itself.
    /// </summary>
    /// <param name="pagingModel"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TProjection"></typeparam>
    /// <returns></returns>
    Task<Paged<TProjection>> GetPagedProjectionByEntityAsync<TProjection>(PagingModel<TEntity> pagingModel,
        FetchModes modes = FetchModes.NoTracking,
        CancellationToken ct = default);

    /// <summary>
    /// Updates the entity by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TModel"></typeparam>
    /// <returns></returns>
    Task UpdateAsync<TModel>(TKey id, TModel model, FetchModes modes = FetchModes.Tracking, CancellationToken ct = default);

    /// <summary>
    /// Bulk update
    /// </summary>
    /// <param name="models"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TModel"></typeparam>
    /// <returns></returns>
    Task UpdateAsync<TModel>(Dictionary<TKey, TModel> models, FetchModes modes = FetchModes.Tracking, CancellationToken ct = default);

    /// <summary>
    /// Hardly deletes the entity by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task DeleteHardAsync(TKey id, FetchModes modes = FetchModes.Tracking, CancellationToken ct = default);

    /// <summary>
    /// Checks if the entity with a given id exists
    /// </summary>
    /// <param name="id"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<bool> ExistsAsync(TKey id, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default);
}
