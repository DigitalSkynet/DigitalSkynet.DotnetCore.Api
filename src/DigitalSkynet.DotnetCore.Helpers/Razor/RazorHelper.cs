using System.Dynamic;
using RazorLight;

namespace DigitalSkynet.DotnetCore.Helpers.Razor;

/// <summary>
/// Class. Represents the Razor Helper
/// </summary>
public class RazorHelper : IRazorHelper
{
    private readonly RazorLightEngine _engine;

    /// <summary>
    /// ctor. Initializes the helper with the engine
    /// </summary>
    /// <param name="engine"></param>
    public RazorHelper(RazorLightEngine engine)
    {
        _engine = engine;
    }

    /// <summary>
    /// Renders a view by the name with the certain model
    /// </summary>
    /// <param name="viewName">Relative path from the root of razore folder (Set in AddRazorRenderer)</param>
    /// <param name="model">The model to be rendered</param>
    /// <typeparam name="TModel">Type of the model</typeparam>
    /// <returns></returns>
    public async Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model)
    {
        var result = await RenderViewToStringAsync<TModel>(viewName, model, default(ExpandoObject));
        return result;
    }

    /// <summary>
    /// Renders a view by the name with the certain model
    /// </summary>
    /// <param name="viewName">Relative path from the root of razore folder (Set in AddRazorRenderer)</param>
    /// <param name="model">The model to be rendered</param>
    /// <param name="viewBag">The view bag dynamic object, you can pas much more data and logic than just a model to the view</param>
    /// <typeparam name="TModel">Type of the model</typeparam>
    /// <returns></returns>
    public async Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model, ExpandoObject viewBag)
    {
        var result = await _engine.CompileRenderAsync<TModel>(viewName, model, viewBag);
        return result;
    }

    /// <summary>
    /// Renders a view by the name with the certain model
    /// </summary>
    /// <param name="viewName">Relative path from the root of razore folder (Set in AddRazorRenderer)</param>
    /// <returns></returns>
    public async Task<string> RenderViewToStringAsync(string viewName)
    {
        var result = await RenderViewToStringAsync(viewName, new { });
        return result;
    }
}