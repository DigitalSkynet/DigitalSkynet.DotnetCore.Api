using System;
using System.IO;
using DigitalSkynet.DotnetCore.DataAccess.Repository;
using DigitalSkynet.DotnetCore.DataAccess.UnitOfWork;
using DigitalSkynet.DotnetCore.Helpers;
using DigitalSkynet.DotnetCore.TestInfrastructure.DependencyInjection;
using DigitalSkynet.DotnetCore.TestInfrastructure.Extensions;
using DigitalSkynet.DotnetCore.Tests.Fakes.DataAccess;
using DigitalSkynet.DotnetCore.Tests.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DigitalSkynet.DotnetCore.Tests;

/// <summary>
/// Startup class for tests. Adds Console Loging, zLocalization
/// </summary>
public class Startup : IDependencyRegistrator
{
    public IServiceProvider GetServiceProvider()
    {
        var services = new ServiceCollection();
        services.AddLogging(logging => logging.AddConsole());
        services.AddLocalization();
        services.AddScoped<EnglishDefaultTestTranslator>();
        services.AddScoped<GermanDefaultTestTransator>();

        var viewsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RazorViews");
        services.AddRazorRenderer(viewsPath);

        RegisterDataAccessLayer(services);

        return services.BuildServiceProvider();
    }

    protected void RegisterDataAccessLayer(IServiceCollection services)
    {
        services.AddInMemoryDbContext<DummyDbContext>();
        services.AddInMemoryDbContext<SeedableDbContext>();
        services.AddScoped(typeof(IGenericRepository<DummyEntity, Guid>), typeof(GenericRepository<DummyDbContext, DummyEntity, Guid>));
        services.AddScoped(typeof(IGenericDeletableRepository<DummyEntity, Guid>), typeof(GenericDeletableRepository<DummyDbContext, DummyEntity, Guid>));
        services.AddScoped(typeof(GenericRepository<DummyDbContext, DummyEntity, Guid>));
        services.AddScoped<IUnitOfWork, UnitOfWork<DummyDbContext>>();

        services.AddAutoMapper(typeof(Startup).Assembly);
    }

}
