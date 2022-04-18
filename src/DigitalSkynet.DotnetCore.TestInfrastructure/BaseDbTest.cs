using DigitalSkynet.DotnetCore.TestInfrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalSkynet.DotnetCore.TestInfrastructure;

/// <summary>
/// Class. Represents the base test with db integration
/// </summary>
/// <typeparam name="TDependencyRegistrator"></typeparam>
/// <typeparam name="TDbContext"></typeparam>
public abstract class BaseDbTest<TDependencyRegistrator, TDbContext> : BaseTest<TDependencyRegistrator>
    where TDependencyRegistrator : IDependencyRegistrator, new()
    where TDbContext : DbContext
{

    /// <summary>
    /// Gets the db context
    /// </summary>
    /// <value></value>
    protected TDbContext DbContext { get; }

    /// <summary>
    /// ctor
    /// </summary>
    protected BaseDbTest()
    {
        DbContext = ServiceProvider.GetRequiredService<TDbContext>();
    }

    #region Interface implementations

    /// <summary>
    /// Initializes the database and seeds it with the data
    /// </summary>
    /// <returns></returns>
    public override async Task InitializeAsync()
    {
        if (DbContext.Database.IsSqlite())
        {
            await DbContext.Database.OpenConnectionAsync();
            await DbContext.Database.EnsureCreatedAsync();
        }

        await SeedAsync();
    }

    /// <summary>
    /// Disposes the test
    /// </summary>
    /// <returns></returns>
    public override async Task DisposeAsync()
    {
        if (DbContext.Database.IsSqlite())
        {
            DbContext.Database.CloseConnection();
            await DbContext.Database.EnsureDeletedAsync();
        }
    }

    #endregion

    /// <summary>
    /// Seeds the database
    /// </summary>
    /// <returns></returns>
    protected virtual Task SeedAsync() => Task.CompletedTask;

    /// <summary>
    /// Ignores the constraints
    /// </summary>
    protected void IgnoreConstraints()
    {
        DbContext.Database.ExecuteSqlRaw("PRAGMA foreign_keys=OFF;");
        DbContext.Database.ExecuteSqlRaw("PRAGMA ignore_check_constraints=true;");
    }
}
