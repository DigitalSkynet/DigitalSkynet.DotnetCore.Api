using DigitalSkynet.DotnetCore.DataStructures.Exceptions.Api;

namespace DigitalSkynet.DotnetCore.Api.Extensions;

/// <summary>
/// Class. Has the extentions methods for logging
/// </summary>
public static class LoggerExtensions
{
    /// <summary>
    /// Logs the api exception
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="exception"></param>
    public static void LogApiException(this ILogger logger, ApiException exception)
    {
        // TODO: improve ApiException logger
        logger.LogError(exception, exception.SystemMessage ?? exception.Message);
    }
}
