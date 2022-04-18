namespace DigitalSkynet.DotnetCore.DataStructures.Localization;

/// <summary>
/// Class. Represents localized result
/// </summary>
public class LocalizedResult
{
    /// <summary>
    /// Gets or sets the translated result
    /// </summary>
    /// <value></value>
    public string Result { get; set; }

    /// <summary>
    /// Gets or sets language info
    /// </summary>
    /// <value></value>
    public LanguageInfo LanguageInfo { get; set; }
}