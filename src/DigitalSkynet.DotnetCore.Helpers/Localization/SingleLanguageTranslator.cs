using DigitalSkynet.DotnetCore.DataStructures.Localization;
using Microsoft.Extensions.Localization;

namespace DigitalSkynet.DotnetCore.Helpers.Localization;

/// <summary>
/// Class. Represents a single language translator
/// </summary>
/// <typeparam name="TCodesEnum"></typeparam>
public class SingleLanguageTranslator<TCodesEnum> : BaseTranslator<TCodesEnum>
    where TCodesEnum : struct, IConvertible
{

    private readonly Languages _defaultLanguage;

    /// <summary>
    /// ctor. Initializes tranlator with the default language
    /// </summary>
    /// <param name="defaultLanguage">Language to be translated to</param>
    /// <param name="factory"></param>
    /// <returns></returns>
    public SingleLanguageTranslator(Languages defaultLanguage, IStringLocalizerFactory factory) : base(factory)
    {
        _defaultLanguage = defaultLanguage;
    }

    /// <summary>
    /// Gets the current language which translator will translate to
    /// </summary>
    public Languages CurrentLanguage => _defaultLanguage;

    /// <summary>
    ///  Translates the array of requests to default language
    /// </summary>
    /// <param name="stringCode"></param>
    /// <param name="replacements"></param>
    /// <returns></returns>
    public override Task<LocalizedResult> Translate(TCodesEnum stringCode, params object[] replacements)
    {
        var result = Translate(_defaultLanguage, stringCode, replacements);
        return Task.FromResult(result);
    }
}