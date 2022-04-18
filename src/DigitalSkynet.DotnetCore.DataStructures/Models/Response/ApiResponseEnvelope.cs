using DigitalSkynet.DotnetCore.DataStructures.Enums.Api;
using DigitalSkynet.DotnetCore.DataStructures.Exceptions.Api;

namespace DigitalSkynet.DotnetCore.DataStructures.Models.Response;

/// <summary>
/// Class. Represetns response envelope
/// </summary>
public class ApiResponseEnvelope
{
    /// <summary>
    /// If `true` the debug info will be included
    /// </summary>
    /// <value></value>
    public static bool IncludeDebugInfo { get; set; } = true;

    #region Constructors

    /// <summary>
    /// ctor. Initializes the envelope with Utc now date as GeneratedAtUtc
    /// </summary>
    public ApiResponseEnvelope()
    {
        GeneratedAtUtc = DateTime.UtcNow;
    }

    /// <summary>
    /// ctor. Initializes the envelope with Utc now date as GeneratedAtUtc
    /// and response status
    /// </summary>
    /// <param name="status">The status of the response</param>
    public ApiResponseEnvelope(ResponseTypes status)
        : this()
    {
        Status = status;
    }

    /// <summary>
    /// ctor. Initializes the envelope with Utc now date as GeneratedAtUtc.
    /// response status, user and system messages and the debug info
    /// </summary>
    /// <param name="status"></param>
    /// <param name="userMessage"></param>
    /// <param name="systemMessage"></param>
    /// <param name="debugInfo"></param>
    public ApiResponseEnvelope(ResponseTypes status, string userMessage, string systemMessage = null, object debugInfo = null)
        : this(status)
    {
        UserMessage = userMessage;

        if (IncludeDebugInfo)
        {
            SystemMessage = systemMessage;
            DebugInfo = debugInfo;
        }
    }

    /// <summary>
    /// ctor. Initilizes the envelope with the given api exception data
    /// </summary>
    /// <param name="exception"></param>
    public ApiResponseEnvelope(ApiException exception)
        : this(exception.ResponseType, exception.Message, exception.SystemMessage)
    {
    }

    /// <summary>
    /// ctor. Initilizes the envelope with the given System exception data
    /// </summary>
    /// <param name="exception"></param>
    public ApiResponseEnvelope(Exception exception)
        : this(ResponseTypes.UnexpectedError, exception.Message, exception.GetType().FullName)
    {
        if (IncludeDebugInfo)
        {
            StackTrace = exception.StackTrace;
        }
    }

    #endregion

    /// <summary>
    /// Gets the status of the response
    /// </summary>
    /// <value></value>
    public ResponseTypes Status { get; protected set; }

    /// <summary>
    /// Gets the user message
    /// </summary>
    /// <value></value>
    public string UserMessage { get; protected set; }

    /// <summary>
    /// Gets the system message
    /// </summary>
    /// <value></value>
    public string SystemMessage { get; protected set; }

    /// <summary>
    /// Gets the stack trace
    /// </summary>
    /// <value></value>
    public string StackTrace { get; protected set; }

    /// <summary>
    /// Gets the debug info
    /// </summary>
    /// <value></value>
    public object DebugInfo { get; protected set; }

    /// <summary>
    /// Gets generated at date and time
    /// </summary>
    /// <value></value>
    public DateTime GeneratedAtUtc { get; private set; }
}
