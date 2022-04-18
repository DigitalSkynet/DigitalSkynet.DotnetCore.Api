using DigitalSkynet.DotnetCore.DataStructures.Enums.Api;
using DigitalSkynet.DotnetCore.DataStructures.Validation;

namespace DigitalSkynet.DotnetCore.DataStructures.Exceptions.Api;

/// <summary>
/// An exception that should be thrown when an API consumer violates some business logic-related validation rules.
/// </summary>
public class ApiValidationException : ApiException
{
    /// <summary>
    /// ctor. Initializes the class with the ValidationResult
    /// </summary>
    /// <param name="validationResult">The result of the validation</param>
    public ApiValidationException(ValidationResult validationResult)
        : base(validationResult?.ToString() ?? "ValidationException")
    {
    }

    /// <summary>
    /// Gets the response type for the error
    /// </summary>
    public override ResponseTypes ResponseType => ResponseTypes.FailedValidation;
}
