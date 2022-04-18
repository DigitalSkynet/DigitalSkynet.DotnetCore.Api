using DigitalSkynet.DotnetCore.DataStructures.Exceptions.Api;

namespace DigitalSkynet.DotnetCore.DataStructures.Validation;

/// <summary>
/// Class. Represents the validation result
/// </summary>
public class ValidationResult
{
    /// <summary>
    /// ctor.
    /// </summary>
    public ValidationResult()
    {
        Issues = new List<ValidationIssue>();
    }

    /// <summary>
    /// ctor. Initializes the result with the issues of given validation result
    /// </summary>
    /// <param name="result"></param>
    public ValidationResult(ValidationResult result)
    {
        Issues = new List<ValidationIssue>(result.Issues);
    }

    /// <summary>
    /// Gets the issues happened during to validation process
    /// </summary>
    /// <value></value>
    public List<ValidationIssue> Issues { get; }

    /// <summary>
    /// Gets the errors happened during to validation process
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ValidationIssue> Errors => Issues.Where(x => x.IsCritical);

    /// <summary>
    /// Gets the warnings happened during to validation process
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ValidationIssue> Warnings => Issues.Where(x => !x.IsCritical);

    /// <summary>
    /// Returns `true` if the result doesn't have Errors
    /// </summary>
    /// <returns></returns>
    public bool IsValid => !Errors.Any();

    /// <summary>
    /// Adds the error to the result
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public ValidationResult AddError(string message)
    {
        Issues.Add(new ValidationIssue { UserMessage = message, IsCritical = true });
        return this;
    }

    /// <summary>
    /// Adds the warning to the result
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public ValidationResult AddWarning(string message)
    {
        Issues.Add(new ValidationIssue { UserMessage = message, IsCritical = false });
        return this;
    }

    /// <summary>
    /// Throws the validation error if the result invalid
    /// </summary>
    public void ThrowIfInvalid()
    {
        if (Errors.Any())
            throw new ApiValidationException(this);
    }

    /// <summary>
    /// Converts the result to string
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return string.Join(" ", Issues.OrderByDescending(x => x.IsCritical));
    }
}
