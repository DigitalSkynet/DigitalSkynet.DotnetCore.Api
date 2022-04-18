using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DigitalSkynet.DotnetCore.DataAccess.Enums;
using DigitalSkynet.DotnetCore.DataStructures.Enums.Api;
using DigitalSkynet.DotnetCore.DataStructures.Exceptions.Api;
using DigitalSkynet.DotnetCore.DataStructures.Models.Paging;
using Microsoft.EntityFrameworkCore;

namespace DigitalSkynet.DotnetCore.DataAccess.Repository;

public class BaseRepository<TDbContext, TEntity> : IBaseRepository<TEntity>
    where TDbContext : DbContext
    where TEntity : class
{
    protected readonly TDbContext _dbContext;
    protected readonly IMapper _mapper;
    public TDbContext DbContext => _dbContext;

    public BaseRepository(TDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates the entity
    /// </summary>
    /// <param name="entity"></param>
    public virtual void Create(TEntity entity) => _dbContext.Add(entity);

    /// <summary>
    /// Maps the entity model to entity and creates
    /// </summary>
    /// <param name="model">Dto Model</param>
    /// <typeparam name="TModel"></typeparam>
    /// <returns></returns>
    public virtual TEntity CreateFromModel<TModel>(TModel model)
    {
        var entity = _mapper.Map<TEntity>(model);
        Create(entity);
        return entity;
    }

    /// <summary>
    /// Creates the list of entities
    /// </summary>
    /// <param name="entities"></param>
    public virtual void Create(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
            Create(entity);
    }

    /// <summary>
    /// Maps the list of entity model to list of entities and creates the list 
    /// </summary>
    /// <param name="models"></param>
    /// <typeparam name="TModel"></typeparam>
    /// <returns></returns>
    public virtual List<TEntity> CreateFromModel<TModel>(IEnumerable<TModel> models)
    {
        var result = _mapper.Map<List<TEntity>>(models);
        Create(result);
        return result;
    }

    public void DetachLocal(TEntity entity)
    {
        var local = _dbContext.Set<TEntity>().Local.FirstOrDefault(x => x == entity);
        if (local != null)
        {
            _dbContext.Entry(entity).State = EntityState.Detached;
        }
    }

    /// <summary>
    /// Updates the entity
    /// </summary>
    /// <param name="entity"></param>
    public virtual void Update(TEntity entity)
    {
        DetachLocal(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    /// <summary>
    /// Updates the list of entities
    /// </summary>
    /// <param name="entities"></param>
    public virtual void Update(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
            Update(entity);
    }

    /// <summary>
    /// Hardly deletes the entity
    /// </summary>
    /// <param name="entity"></param>
    public virtual void DeleteHard(TEntity entity) => _dbContext.Remove(entity);

    /// <summary>
    /// Hardly deletes the list of entities
    /// </summary>
    /// <param name="entities"></param>
    public virtual void DeleteHard(IEnumerable<TEntity> entities) => _dbContext.RemoveRange(entities);

    /// <summary>
    /// Checks if the database has the entities according to predicate
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name=""></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public virtual Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default)
    {
        return GetBaseQuery(modes).AnyAsync(predicate, ct);
    }

    /// <summary>
    /// Finds the first occurence by a given predicate
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<TEntity> FindFirstAsync(Expression<Func<TEntity, bool>> predicate, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default)
    {
        var entity = await GetBaseQuery(modes).FirstOrDefaultAsync(predicate);
        return entity;
    }


    /// <summary>
    /// Filters the entities by predicate and returns paged result
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="sortings"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public virtual async Task<Paged<TEntity>> FilterPagedAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize, IEnumerable<SortModel> sortings = default, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default)
    {
        var query = Filter(predicate, modes);
        var orderedQuery = GetOrderedQuery(query, sortings);
        var count = await orderedQuery.CountAsync(ct);
        var items = await orderedQuery.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync(ct);
        var result = new Paged<TEntity>(items, count, pageNumber, pageSize);
        return result;
    }

    /// <summary>
    /// Filters the set of entities and returns paged projection of the result
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="sortings"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TProjection"></typeparam>
    /// <returns></returns>
    public virtual async Task<Paged<TProjection>> ProjectPagedToAsync<TProjection>(Expression<Func<TProjection, bool>> predicate, int pageNumber, int pageSize, IEnumerable<SortModel> sortings = default, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default)
    {
        var projected = GetBaseQuery(modes)
            .ProjectTo<TProjection>(_mapper.ConfigurationProvider)
            .Where(predicate);

        var result = await GetPagedFromProjection(projected, pageNumber, pageSize, sortings, ct);
        return result;
    }

    public virtual async Task<Paged<TProjection>> ProjectPagedByEntityToAsync<TProjection>(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize, IEnumerable<SortModel> sortings = default, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default)
    {
        var projected = Filter(predicate, modes)
            .ProjectTo<TProjection>(_mapper.ConfigurationProvider);

        var result = await GetPagedFromProjection(projected, pageNumber, pageSize, sortings, ct);
        return result;
    }

    /// <summary>
    /// Filters the data by a given predicate
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public virtual Task<List<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> predicate, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default)
        => Filter(predicate, modes).ToListAsync(ct);

    /// <summary>
    /// Projects the filtered results by given predicate to a List of custom model type
    /// </summary>
    /// <param name="predicate">Predicate by entity</param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TProjection"></typeparam>
    /// <returns></returns>
    public virtual Task<List<TProjection>> ProjectToAsync<TProjection>(Expression<Func<TEntity, bool>> predicate, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default)
        => Filter(predicate, modes).ProjectTo<TProjection>(_mapper.ConfigurationProvider).ToListAsync(ct);


    /// <summary>
    /// Projects the filtered results by given predicate to a List of custom model type
    /// </summary>
    /// <param name="predicate">Predicate by projection</param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TProjection"></typeparam>
    /// <returns></returns>
    public virtual Task<List<TProjection>> ProjectToByProjectionAsync<TProjection>(Expression<Func<TProjection, bool>> predicate, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default)
        => GetBaseQuery(modes).ProjectTo<TProjection>(_mapper.ConfigurationProvider).Where(predicate).ToListAsync(ct);


    public virtual async Task<Paged<TProjection>> GetPagedFromProjection<TProjection>(IQueryable<TProjection> projected, int pageNumber, int pageSize, IEnumerable<SortModel> sortings = default, CancellationToken ct = default)
    {
        var count = await projected.CountAsync(ct);
        var ordered = GetOrderedQuery(projected, sortings);
        var items = await ordered
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(ct);

        var result = new Paged<TProjection>(items, count, pageNumber, pageSize);
        return result;
    }

    /// <summary>
    /// Filters data by expression predicate
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    protected virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate, FetchModes modes = FetchModes.NoTracking)
                => GetBaseQuery(modes).Where(predicate);


    /// <summary>
    /// Gets the base query. if IsWithNoTracking is true will apply all queries as non-tracking
    /// </summary>
    /// <returns></returns>
    protected virtual IQueryable<TEntity> GetBaseQuery(FetchModes modes)
    {
        IQueryable<TEntity> query = _dbContext.Set<TEntity>();
        if (!modes.HasFlag(FetchModes.Tracking))
        {
            query = query.AsNoTracking();
        }
        return query;
    }

    protected IQueryable<TQuery> GetOrderedQuery<TQuery>(IQueryable<TQuery> filteredQuery, IEnumerable<SortModel> sortings)
    {
        var orderedQuery = filteredQuery;
        if (sortings?.Any() == true)
        {
            var type = typeof(TQuery);
            var orderByCommand = sortings.First().Direction == SortDirections.Asc ? "OrderBy" : "OrderByDescending";
            var isOrderedQuery = false;
            foreach (var sorting in sortings)
            {
                var property = type.GetProperty(sorting.FieldName, BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public);
                if (property == null)
                {
                    throw new ApiNotFoundException("Property is not found");
                }
                if (isOrderedQuery)
                {
                    orderByCommand = sorting.Direction == SortDirections.Asc ? "ThenBy" : "ThenByDescending"; // second and all further orderings must be ThenBy
                }
                var parameter = Expression.Parameter(type, "p");
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExpression = Expression.Lambda(propertyAccess, parameter);
                var resultExpression = Expression.Call(typeof(Queryable), orderByCommand, new Type[] { type, property.PropertyType },
                    orderedQuery.Expression, Expression.Quote(orderByExpression));
                orderedQuery = orderedQuery.Provider.CreateQuery<TQuery>(resultExpression);
                isOrderedQuery = true;
            }
        }
        return orderedQuery;
    }

    public virtual Task<TProjection> GetFirstOrDefaultProjection<TProjection>(Expression<Func<TEntity, bool>> predicate, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default)
       => Filter(predicate, modes)
               .ProjectTo<TProjection>(_mapper.ConfigurationProvider)
               .FirstOrDefaultAsync(ct);

    public virtual Task<TProjection> GetSingleOrDefaultProjection<TProjection>(Expression<Func<TEntity, bool>> predicate, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default)
      => Filter(predicate, modes)
              .ProjectTo<TProjection>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(ct);

    public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default)
        => Filter(predicate, modes).CountAsync(ct);

    public TEntity MapToEntity<TModel>(TModel model)
    {
        var entity = _mapper.Map<TEntity>(model);
        return entity;
    }
}