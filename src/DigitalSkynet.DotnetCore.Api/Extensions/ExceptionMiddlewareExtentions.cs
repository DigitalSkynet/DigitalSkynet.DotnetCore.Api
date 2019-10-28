using DigitalSkynet.DotnetCore.Api.Middleware;
using Microsoft.AspNetCore.Builder;

namespace DigitalSkynet.DotnetCore.Api.Extensions
{
    public static class ExceptionMiddlewareExtentions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
