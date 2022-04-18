using DigitalSkynet.DotnetCore.TestInfrastructure.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DigitalSkynet.DotnetCore.TestInfrastructure;

/// <summary>
/// Class. Represetns the base test
/// </summary>
/// <typeparam name="TDependencyRegistrator"></typeparam>
public abstract class BaseTest<TDependencyRegistrator> : IAsyncLifetime, IDisposable
    where TDependencyRegistrator : IDependencyRegistrator, new()
{
    #region Private fields

    private static readonly Lazy<IServiceProvider> _serviceProvider
        = new Lazy<IServiceProvider>(() => new TDependencyRegistrator().GetServiceProvider(),
            LazyThreadSafetyMode.ExecutionAndPublication);
    private readonly IServiceScope _serviceScope;

    #endregion

    /// <summary>
    /// Gets the service provider
    /// </summary>
    protected IServiceProvider ServiceProvider => _serviceScope.ServiceProvider;

    /// <summary>
    /// ctor.
    /// </summary>
    protected BaseTest()
    {
        _serviceScope = _serviceProvider.Value.CreateScope();
    }

    /// <summary>
    /// Initializes the test
    /// </summary>
    /// <returns></returns>
    public virtual Task InitializeAsync() => Task.CompletedTask;

    /// <summary>
    /// Disposes the test
    /// </summary>
    /// <returns></returns>
    public virtual Task DisposeAsync() => Task.CompletedTask;

    /// <summary>
    /// Disposes the test
    /// </summary>

    public void Dispose()
    {
        _serviceScope?.Dispose();
    }
}
