namespace DigitalSkynet.DotnetCore.DataStructures.Localization.Interfaces;

/// <summary>
/// Interface. Describes the lanaguage translator
/// </summary>
/// <typeparam name="TCodesEnum">The type of codes of phrases to be translated</typeparam>
public interface ITranslator<TCodesEnum>
    where TCodesEnum : struct, IConvertible
{

    /// <summary>
    /// Gets the result of translation with auto-detected language
    /// </summary>
    /// <param name="stringCode">The code of resource to translate</param>
    /// <param name="replacements">Replacements in the translated string, e.g {0}, {1}, {2}</param>
    /// <returns></returns>
    Task<LocalizedResult> Translate(TCodesEnum stringCode, params object[] replacements);

    /// <summary>
    /// Gets the result of multi-translations, organized into dictionary with auto-detected language
    /// </summary>
    /// <param name="translateRequests">The list of translation requests</param>
    /// <returns></returns>
    Task<Dictionary<TCodesEnum, LocalizedResult>> Translate(params TranslateRequest[] translateRequests);

    /// <summary>
    /// Gets the result of translation with explicit language
    /// </summary>
    /// <param name="language">Language to translate to</param>
    /// <param name="stringCode">The code of resource to translate</param>
    /// <param name="replacements">Replacements in the translated string, e.g {0}, {1}, {2}</param>
    /// <returns></returns>
    LocalizedResult Translate(Languages language, TCodesEnum stringCode, params object[] replacements);

    /// <summary>
    /// Gets the result of multi-translations, organized into dictionary with explicit language
    /// </summary>
    /// <param name="language">Language to translate to</param>
    /// <param name="translateRequests">The list of translation requests</param>
    /// <returns></returns>
    Dictionary<TCodesEnum, LocalizedResult> Translate(Languages language, params TranslateRequest[] translateRequests);
}