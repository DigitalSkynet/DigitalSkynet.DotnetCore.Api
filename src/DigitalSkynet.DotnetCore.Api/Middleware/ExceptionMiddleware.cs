using System;
using System.Threading.Tasks;
using DigitalSkynet.DotnetCore.Api.Exceptions;
using DigitalSkynet.DotnetCore.Api.Logging;
using Microsoft.AspNetCore.Http;

namespace DigitalSkynet.DotnetCore.Api.Middleware
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invokes a request. Logs exception only when not DEUBUGING
        /// </summary>
        /// <param name="httpContext">Current context</param>
        /// <param name="logErrorsService">Logging service</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext, ILogErrorsService logErrorsService)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
#if !DEBUG
                await logErrorsService.CreateLog(ex);
#endif

                ApiException webSiteException = new ApiException(ex.Message);
                if (ex is ApiException)
                    webSiteException = ex as ApiException;

                await OverrideResponse(webSiteException, httpContext);
            }
        }

        protected async Task OverrideResponse(ApiException exception, HttpContext httpContext)
        {
            httpContext.Response.Clear();
            httpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            httpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            httpContext.Response.StatusCode = exception.StatusCode;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsync(exception.Message);
        }
    }
}
