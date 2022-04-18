using System.Text.Json;
using DigitalSkynet.DotnetCore.Api.Extensions;
using DigitalSkynet.DotnetCore.DataStructures.Enums.Api;
using DigitalSkynet.DotnetCore.DataStructures.Exceptions.Api;
using DigitalSkynet.DotnetCore.DataStructures.Models.Response;

namespace DigitalSkynet.DotnetCore.Api.Middleware;

/// <summary>
/// Class. Represents base exception handling middleware
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    /// <summary>
    /// ctor. Initializes middleware
    /// </summary>
    /// <param name="next"></param>
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Invokes a request. Logs exceptions to the standard ILogger.
    /// </summary>
    public async Task Invoke(HttpContext httpContext, ILoggerFactory loggerFactory)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<ExceptionMiddleware>();

            if (ex is ApiException apiException)
            {
                logger.LogApiException(apiException);
                await OverrideResponse(new ApiResponseEnvelope(apiException), httpContext);
            }
            else
            {
                logger.LogError(ex, "Unexpected exception");
                await OverrideResponse(new ApiResponseEnvelope(ex), httpContext);
            }
        }
    }

    /// <summary>
    /// Overrides the response and add the status code and data of ApiResponseEnvelope
    /// </summary>
    /// <param name="response"></param>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    protected async Task OverrideResponse(ApiResponseEnvelope response, HttpContext httpContext)
    {
        if (!httpContext.Response.HasStarted)
        {
            httpContext.Response.Clear();
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)response.Status.ToHttpStatusCode();
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response, typeof(ApiResponseEnvelope)));
        }
        else
            throw new ApiInvalidOperationException(response.UserMessage,
                "The response stream has already started. Cannot override it with the exception message");
    }
}

/// <summary>
/// Class. Represents the exception handling middleware
/// </summary>
public static class ExceptionMiddlewareExtentions
{


    /// <summary>
    /// Makes the app builder to use the ExceptionMiddleware
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="includeDebugInfo"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder, bool includeDebugInfo = true)
    {
        ApiResponseEnvelope.IncludeDebugInfo = includeDebugInfo;
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}
