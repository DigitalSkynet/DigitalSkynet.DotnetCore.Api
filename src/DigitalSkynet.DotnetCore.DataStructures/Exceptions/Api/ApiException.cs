using System;
using DigitalSkynet.DotnetCore.DataStructures.Enums.Api;

namespace DigitalSkynet.DotnetCore.DataStructures.Exceptions.Api;

/// <summary>
/// A base class for all custom exceptions you may use to distinguish them from system ones.
/// You may process custom exceptions a different way than system exceptions, they may even be expected
/// and be converted to a predefined HTTP status code (such as Not Found).
/// </summary>
public abstract class ApiException : Exception
{
    /// <summary>
    /// ctor. Initializes the class with given user and system message
    /// </summary>
    /// <param name="userMessage">The message which will be shown to user</param>
    /// <param name="systemMessage">The message which will be logged</param>
    protected ApiException(string userMessage, string systemMessage = null)
        : base(userMessage)
    {
        SystemMessage = systemMessage;
    }

    /// <summary>
    /// A type of the API response for this exception.
    /// </summary>
    public abstract ResponseTypes ResponseType { get; }
    /// <summary>
    /// A system message which is not safe to show on UI but may be useful for debugging.
    /// </summary>
    public string SystemMessage { get; protected set; }

}
