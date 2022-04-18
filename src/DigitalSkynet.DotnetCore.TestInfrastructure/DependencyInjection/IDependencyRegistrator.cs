namespace DigitalSkynet.DotnetCore.TestInfrastructure.DependencyInjection;

/// <summary>
/// An interface that represents a class which can be used by tests to arrange a DI container
/// </summary>
public interface IDependencyRegistrator
{
    /// <summary>
    /// Returns a built DI container (service provider)
    /// </summary>
    IServiceProvider GetServiceProvider();
}
