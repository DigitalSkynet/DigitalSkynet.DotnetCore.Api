using DigitalSkynet.DotnetCore.DataStructures.Enums.Api;

namespace DigitalSkynet.DotnetCore.DataStructures.Exceptions.Api;

/// <summary>
/// An exception that should be thrown when an anonimous user tries to access API that requires authentication.
/// </summary>
public class ApiNotAuthenticatedException : ApiException
{
    /// <summary>
    /// ctor. Initializes the class with given user and system message
    /// </summary>
    /// <param name="userMessage">The message which will be shown to user</param>
    /// <param name="systemMessage">The message which will be logged</param>
    public ApiNotAuthenticatedException(string userMessage, string systemMessage = null)
        : base(userMessage, systemMessage)
    { }

    /// <summary>
    /// Gets the response type for the error
    /// </summary>
    public override ResponseTypes ResponseType => ResponseTypes.NotAuthenticated;
}
