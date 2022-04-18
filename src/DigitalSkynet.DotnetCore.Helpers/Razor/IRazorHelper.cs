using System.Dynamic;

namespace DigitalSkynet.DotnetCore.Helpers.Razor;

/// <summary>
/// Interface. Describes the razor helper
/// </summary>
public interface IRazorHelper
{
    /// <summary>
    /// Renders a view by the name with the certain model
    /// </summary>
    /// <param name="viewName">Relative path from the root of razore folder (Set in AddRazorRenderer)</param>
    /// <param name="model">The model to be rendered</param>
    /// <typeparam name="TModel">Type of the model</typeparam>
    /// <returns></returns>
    Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);

    /// <summary>
    /// Renders a view by the name with the certain model
    /// </summary>
    /// <param name="viewName">Relative path from the root of razore folder (Set in AddRazorRenderer)</param>
    /// <param name="model">The model to be rendered</param>
    /// <param name="viewBag">The view bag dynamic object, you can pas much more data and logic than just a model to the view</param>
    /// <typeparam name="TModel">Type of the model</typeparam>
    /// <returns></returns>
    Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model, ExpandoObject viewBag);

    /// <summary>
    /// Renders a view by the name with the certain model
    /// </summary>
    /// <param name="viewName">Relative path from the root of razore folder (Set in AddRazorRenderer)</param>
    /// <returns></returns>
    Task<string> RenderViewToStringAsync(string viewName);
}