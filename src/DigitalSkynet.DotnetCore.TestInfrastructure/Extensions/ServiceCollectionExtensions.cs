using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalSkynet.DotnetCore.TestInfrastructure.Extensions;

/// <summary>
/// Class. Represents the service collection extensions
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds sqlite db context with InMemory mode
    /// </summary>
    /// <param name="services"></param>
    /// <typeparam name="TDbContext"></typeparam>
    /// <returns></returns>
    public static IServiceCollection AddInMemoryDbContext<TDbContext>(this IServiceCollection services)
        where TDbContext : DbContext
    {
        services.AddDbContext<TDbContext>(options =>
        {
            options.EnableSensitiveDataLogging();
            options.UseSqlite(new SqliteConnectionStringBuilder { Mode = SqliteOpenMode.Memory }.ConnectionString);
        });
        return services;
    }
}
