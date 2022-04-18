using DigitalSkynet.DotnetCore.DataStructures.Localization.Interfaces;

namespace DigitalSkynet.DotnetCore.DataStructures.Localization;

/// <summary>
/// Class. Represents language info
/// </summary>
public class LanguageInfo : ILanguageInfo
{
    #region Fields definition

    private readonly Languages _language;
    private readonly Dictionary<Languages, string> _codes;

    #endregion
    /// <summary>
    /// ctor. Initializes class with the given language
    /// </summary>
    /// <param name="language"></param>
    public LanguageInfo(Languages language)
    {
        _codes = GetSupportedCodes();

        if (!_codes.ContainsKey(language))
        {
            throw new ArgumentException($"{nameof(LanguageInfo)} does not contain code for {language} language.");
        }

        _language = language;
    }

    /// <summary>
    /// Gets the language id
    /// </summary>
    /// <returns></returns>
    public int Id => (int)_language;

    /// <summary>
    /// Gets the human readable language name
    /// </summary>
    /// <returns></returns>
    public string Name => _language.ToString();

    /// <summary>
    /// Gets the language code
    /// </summary>
    /// <returns></returns>
    public string Code => _codes.GetValueOrDefault(_language);

    /// <summary>
    /// Gets the language
    /// </summary>
    public Languages Language => _language;

    /// <summary>
    /// Gets supported language codes
    /// </summary>
    /// <returns></returns>
    public Dictionary<Languages, string> GetSupportedCodes()
    {
        return new Dictionary<Languages, string>() {
            {Languages.English, "en-US"},
            {Languages.German, "de-DE"},
            {Languages.French, "fr-FR"},
            {Languages.Russian, "ru-RU"},
            {Languages.Spanish, "es-US"}
        };
    }
}