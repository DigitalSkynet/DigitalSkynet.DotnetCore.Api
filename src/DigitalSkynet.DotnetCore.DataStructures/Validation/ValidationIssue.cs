using System.Diagnostics;

namespace DigitalSkynet.DotnetCore.DataStructures.Validation;

/// <summary>
/// Class. Represents the validation issue
/// </summary>
[DebuggerDisplay("{UserMessage} | critical: {IsCritical}")]
public class ValidationIssue
{
    /// <summary>
    /// Gets or sets the message to be shown to user
    /// </summary>
    /// <value></value>
    public string UserMessage { get; set; }

    /// <summary>
    /// If `true` means the issues is critical
    /// </summary>
    /// <value></value>
    public bool IsCritical { get; set; }

    /// <summary>
    /// Converts the issue to string
    /// </summary>
    /// <returns></returns>

    public override string ToString() => UserMessage;
}
