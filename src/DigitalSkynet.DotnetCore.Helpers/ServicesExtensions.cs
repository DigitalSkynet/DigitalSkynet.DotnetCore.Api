using DigitalSkynet.DotnetCore.Helpers.Razor;
using Microsoft.Extensions.DependencyInjection;
using RazorLight;

namespace DigitalSkynet.DotnetCore.Helpers;

/// <summary>
/// Class. Represents the Service Collection extensions
/// </summary>
public static class ServicesExtensions
{
    /// <summary>
    /// Adds the razor renderer to services
    /// </summary>
    /// <param name="services"></param>
    /// <param name="razorViewsPath">The path where all the razor views are stored</param>
    public static void AddRazorRenderer(this IServiceCollection services, string razorViewsPath)
    {
        services.AddScoped<IRazorHelper, RazorHelper>();
        services.AddScoped(_provider =>
                   {
                       RazorLightEngine engine = new RazorLightEngineBuilder()
                           .DisableEncoding()
                           .UseFileSystemProject(razorViewsPath)
                           .UseMemoryCachingProvider()
                           .Build();

                       return engine;
                   });
    }
}