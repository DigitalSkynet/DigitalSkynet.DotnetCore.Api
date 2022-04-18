namespace DigitalSkynet.DotnetCore.DataStructures.Localization;

/// <summary>
/// Supported languages
/// </summary>
public class TranslateRequest
{
    private readonly IConvertible _codesEnum;
    private readonly object[] _replacements;

    /// <summary>
    /// Constructor. Initializes request with params
    /// </summary>
    /// <param name="codesEnum">The code of resource to translate</param>
    /// <param name="replacements">Replacements in the translated string, e.g {0}, {1}, {2}</param>
    public TranslateRequest(Enum codesEnum, params object[] replacements)
    {
        _codesEnum = codesEnum;
        _replacements = replacements;
    }

    /// <summary>
    /// Gets the codes enumeration
    /// </summary>
    public IConvertible CodesEnum => _codesEnum;

    /// <summary>
    /// Gets the replacements in the phrase to be translated
    /// </summary>
    public object[] Replacements => _replacements;
}