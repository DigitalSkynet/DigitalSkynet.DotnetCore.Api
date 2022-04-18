using DigitalSkynet.DotnetCore.DataStructures.Generics;

namespace DigitalSkynet.DotnetCore.DataAccess.StaticData;

/// <summary>
/// A base class to incapsulate static data collection for using in tests or database seedings
/// </summary>
/// <typeparam name="TEntity">The type of entity</typeparam>
public abstract class StaticDataProvider<TEntity>
    where TEntity : class
{
    protected readonly List<TEntity> _entities;

    protected StaticDataProvider()
    {
        _entities = GenerateEntities().ToList();
    }

    public IReadOnlyList<TEntity> Entities => _entities;

    protected abstract IEnumerable<TEntity> GenerateEntities();
}

/// <summary>
/// A base class to incapsulate static data collection for using in tests or database seedings
/// </summary>
/// <typeparam name="TEntity">The type of entity</typeparam>
/// <typeparam name="TProvider">The type of derived provider</typeparam>
public abstract class StaticDataProvider<TProvider, TEntity> : StaticDataProvider<TEntity>
    where TProvider : StaticDataProvider<TEntity>
    where TEntity : class
{
    public static TProvider Instance => LazySingleton<TProvider>.Instance;
}
