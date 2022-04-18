using DigitalSkynet.DotnetCore.DataStructures.Interfaces;

namespace DigitalSkynet.DotnetCore.DataAccess.Repository;

/// <summary>
/// Interface. Describes deletable repository
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IGenericDeletableRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
    where TEntity : class, ISoftDeletable, IHasKey<TKey>
    where TKey : struct
{
    /// <summary>
    /// Softly deletes the entity
    /// </summary>
    /// <param name="entity"></param>
    void Delete(TEntity entity);

    /// <summary>
    /// Softly deletes the collection of entities
    /// </summary>
    /// <param name="entity"></param>
    void Delete(IEnumerable<TEntity> entities);

    /// <summary>
    /// Softly deletes the entity by id
    /// </summary>
    /// <param name="entity"></param>
    Task DeleteAsync(TKey id, CancellationToken ct = default);
}
