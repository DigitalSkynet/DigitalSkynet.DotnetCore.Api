using AutoMapper;
using DigitalSkynet.DotnetCore.DataAccess.Enums;
using DigitalSkynet.DotnetCore.DataStructures.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DigitalSkynet.DotnetCore.DataAccess.Repository;

/// <summary>
/// Class. Represents soft-deletable repository. Base query by default ignores all data where IsDeleted set to true
/// </summary>
/// <typeparam name="TDbContext"></typeparam>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public class GenericDeletableRepository<TDbContext, TEntity, TKey> : GenericRepository<TDbContext, TEntity, TKey>, IGenericDeletableRepository<TEntity, TKey>
    where TDbContext : DbContext
    where TEntity : class, ISoftDeletable, IHasKey<TKey>
    where TKey : struct
{

    /// <summary>
    /// Initializes repository
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="mapper"></param>
    /// <returns></returns>
    public GenericDeletableRepository(TDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    /// <summary>
    /// Gets the baes query. if IncludesDeleted set to true will include deleted data
    /// </summary>
    /// <returns></returns>
    protected override IQueryable<TEntity> GetBaseQuery(FetchModes modes)
    {
        var query = base.GetBaseQuery(modes);
        if (!modes.HasFlag(FetchModes.FindDeleted))
        {
            query = query.Where(x => !x.IsDeleted);
        }
        return query;
    }

    /// <summary>
    /// Softly deletes the entity
    /// </summary>
    /// <param name="entity"></param>
    public void Delete(TEntity entity)
    {
        entity.IsDeleted = true;
        Update(entity);
    }

    /// <summary>
    /// Softly deletes the collection
    /// </summary>
    /// <param name="entities"></param>
    public void Delete(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
            Delete(entity);
    }

    /// <summary>
    /// Softly deletes the entity by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task DeleteAsync(TKey id, CancellationToken ct = default)
    {
        var entity = await GetByIdAsync(id, FetchModes.Tracking, ct: ct);
        if (entity != null)
        {
            entity.IsDeleted = true;
        }
    }
}
