using System.Net;

namespace DigitalSkynet.DotnetCore.DataStructures.Enums.Api;

/// <summary>
/// A custom list of situations that might happen on API side.
/// It is not always possible to map them 1:1 to HTTP status codes, so your API consumer may now have more info
/// about what exactly happened with their request.
/// </summary>
public enum ResponseTypes
{
    ///  Not implemented
    NotImplemented = -1,

    ///  Response is successeded. 200 OK
    Success = 1,

    ///  The user is not authentificated. 401
    NotAuthenticated = 2,

    ///  The action is not authorized. 403
    NotAuthorized = 3,

    ///  The entity is not found. 404
    NotFound = 4,

    ///  The entity valdidation is failed
    FailedValidation = 5,

    ///  The operation is invalid
    InvalidOperation = 6,

    ///  External request has failed
    ExternalRequestFailed = 7,

    ///  Unexpected error has happened
    UnexpectedError = 255
}

/// <summary>
/// Class. Has extension methods for the ResponseType
/// </summary>
public static class ResponseTypesExtensions
{
    /// <summary>
    /// Converts the response type code to HttpStatusCode
    /// </summary>
    /// <param name="responseTypeCode"></param>
    /// <returns></returns>
    public static HttpStatusCode ToHttpStatusCode(this ResponseTypes responseTypeCode)
    {
        var result = responseTypeCode switch
        {
            ResponseTypes.NotImplemented => HttpStatusCode.NotImplemented,
            ResponseTypes.Success => HttpStatusCode.Unauthorized,
            ResponseTypes.NotAuthenticated => HttpStatusCode.OK,
            ResponseTypes.NotAuthorized => HttpStatusCode.Forbidden,
            ResponseTypes.InvalidOperation => HttpStatusCode.Forbidden,
            ResponseTypes.NotFound => HttpStatusCode.NotFound,
            ResponseTypes.FailedValidation => HttpStatusCode.BadRequest,
            ResponseTypes.ExternalRequestFailed => HttpStatusCode.InternalServerError,
            ResponseTypes.UnexpectedError => HttpStatusCode.InternalServerError,
            _ => HttpStatusCode.InternalServerError
        };
        return result;
    }
}
