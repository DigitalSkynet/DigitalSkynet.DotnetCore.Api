using System.Linq.Expressions;
using DigitalSkynet.DotnetCore.DataAccess.Enums;
using DigitalSkynet.DotnetCore.DataStructures.Models.Paging;

namespace DigitalSkynet.DotnetCore.DataAccess.Repository;

public interface IBaseRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Returns first entity by predicate
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<TEntity> FindFirstAsync(Expression<Func<TEntity, bool>> predicate, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default);

    /// <summary>
    /// Gets the count of items filtered by predicate
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default);

    /// <summary>
    /// Creates the entity
    /// </summary>
    /// <param name="entity"></param>
    void Create(TEntity entity);

    /// <summary>
    /// Creates the collection of entities
    /// </summary>
    /// <param name="entities"></param>
    void Create(IEnumerable<TEntity> entities);

    /// <summary>
    ///  Filters the data by a given predicate
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<List<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> predicate, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default);

    /// <summary>
    /// Filters data and returns paged result
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<Paged<TEntity>> FilterPagedAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize, IEnumerable<SortModel> sortings = default, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default);

    /// <summary>
    /// Maps the model to entity
    /// </summary>
    /// <param name="model"></param>
    /// <typeparam name="TModel"></typeparam>
    /// <returns></returns>
    TEntity MapToEntity<TModel>(TModel model);

    /// <summary>
    /// Gets paged data and projects it to given type. The data gets  sorted in the DB after projection
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="sortings"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TProjection"></typeparam>
    /// <returns></returns>
    Task<Paged<TProjection>> ProjectPagedToAsync<TProjection>(Expression<Func<TProjection, bool>> predicate, int pageNumber, int pageSize, IEnumerable<SortModel> sortings = default, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default);

    /// <summary>
    /// Gets paged data and projects it to given type. The data gets  sorted in the DB before projection
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="sortings"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TProjection"></typeparam>
    /// <returns></returns>
    Task<Paged<TProjection>> ProjectPagedByEntityToAsync<TProjection>(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize, IEnumerable<SortModel> sortings = default, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default);
    /// <summary>
    /// Projects the filtered results by given predicate to a List of custom model type
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TProjection"></typeparam>
    /// <returns></returns>
    Task<List<TProjection>> ProjectToAsync<TProjection>(Expression<Func<TEntity, bool>> predicate, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default);

    /// <summary>
    /// Projects the filtered results by given predicate to a List of custom model type
    /// </summary>
    /// <param name="predicate">Predicate by projection</param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TProjection"></typeparam>
    /// <returns></returns>
    Task<List<TProjection>> ProjectToByProjectionAsync<TProjection>(Expression<Func<TProjection, bool>> predicate, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default);

    /// <summary>
    /// Gets first or default projection by predicate
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TProjection"></typeparam>
    /// <returns></returns>
    Task<TProjection> GetFirstOrDefaultProjection<TProjection>(Expression<Func<TEntity, bool>> predicate, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default);

    /// <summary>
    /// Gets single or default projection by predicate
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TProjection"></typeparam>
    /// <returns></returns>
    Task<TProjection> GetSingleOrDefaultProjection<TProjection>(Expression<Func<TEntity, bool>> predicate, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default);

    /// <summary>
    /// Creates the entity
    /// </summary>
    /// <param name="model"></param>
    /// <typeparam name="TModel"></typeparam>
    /// <returns></returns>
    TEntity CreateFromModel<TModel>(TModel model);

    /// <summary>
    /// Creates the collection of entities
    /// </summary>
    /// <param name="models"></param>
    /// <typeparam name="TModel"></typeparam>
    /// <returns></returns>
    List<TEntity> CreateFromModel<TModel>(IEnumerable<TModel> models);

    /// <summary>
    /// Updates the collection of entities
    /// </summary>
    /// <param name="entities"></param>
    void Update(IEnumerable<TEntity> entities);

    /// <summary>
    /// Updates the entity 
    /// </summary>
    /// <param name="entities"></param>
    void Update(TEntity entities);

    /// <summary>
    /// Hardly deletes the entity from the database
    /// </summary>
    /// <param name="entity"></param>
    void DeleteHard(TEntity entity);

    /// <summary>
    /// Hardly deletes the IEnumerable of entities
    /// </summary>
    /// <param name="entities"></param>
    void DeleteHard(IEnumerable<TEntity> entities);

    /// <summary>
    /// Returns true if there is at least one element by predicate
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="modes"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, FetchModes modes = FetchModes.NoTracking, CancellationToken ct = default);
}
