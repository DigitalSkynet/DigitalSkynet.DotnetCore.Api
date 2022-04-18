using System.Globalization;
using DigitalSkynet.DotnetCore.DataStructures.Localization;
using DigitalSkynet.DotnetCore.DataStructures.Localization.Interfaces;
using DigitalSkynet.DotnetCore.Helpers.Strings;
using Microsoft.Extensions.Localization;

namespace DigitalSkynet.DotnetCore.Helpers.Localization;

/// <summary>
/// Translator service. To use the service inherit from this class and implement abstract method. 
/// Create the resx files on the same level of your implementation with the same name, by adding language code.
/// For example MyTranslator will search for the resx files first like MyTranslator.en-US.resx for english language
/// </summary>
/// <typeparam name="TCodesEnum"></typeparam>
public abstract class BaseTranslator<TCodesEnum> : ITranslator<TCodesEnum>
where TCodesEnum : struct, IConvertible
{

    /// <summary>
    /// Localizer factory
    /// </summary>
    protected readonly IStringLocalizerFactory _factory;

    /// <summary>
    /// ctor. Initializes translator with the factory
    /// </summary>
    /// <param name="factory"></param>
    protected BaseTranslator(IStringLocalizerFactory factory)
    {
        _factory = factory;
    }

    /// <summary>
    /// Automatically detects language and translates
    /// </summary>
    /// <param name="stringCode"></param>
    /// <param name="replacements"></param>
    /// <returns></returns>
    public abstract Task<LocalizedResult> Translate(TCodesEnum stringCode, params object[] replacements);

    /// <summary>
    /// Translates explicitly
    /// </summary>
    /// <param name="language">Language to translate to</param>
    /// <param name="stringCode">Code of the string in your resx</param>
    /// <param name="replacements">Array of replacements in the string</param>
    /// <returns></returns>
    public LocalizedResult Translate(Languages language, TCodesEnum stringCode, params object[] replacements)
    {
        var isCodesEnum = typeof(Enum).IsInstanceOfType(stringCode);

        if (!isCodesEnum)
        {
            throw new ArgumentException("stringCode must be enum!");
        }

        var originalCulture = CultureInfo.CurrentCulture;

        var languageDescription = new LanguageInfo(language);
        var newCulture = new CultureInfo(languageDescription.Code);
        CultureInfo.CurrentCulture = newCulture;
        CultureInfo.CurrentUICulture = newCulture;

        var type = GetType();
        var localizer = _factory.Create(type);

        var translated = localizer
            .GetString(stringCode.ToString())
            .Value;


        CultureInfo.CurrentCulture = originalCulture;
        CultureInfo.CurrentUICulture = originalCulture;

        switch (language)
        {
            case Languages.German:
            case Languages.French:
            case Languages.Spanish:
                translated = translated?.EncodeNonAsciiCharacters();
                break;

            default:
                break;
        }

        var result = new LocalizedResult
        {
            Result = string.Format(translated, replacements),
            LanguageInfo = languageDescription
        };

        return result;
    }

    /// <summary>
    /// Translates the request to a given language
    /// </summary>
    /// <param name="language">Language to be translated to</param>
    /// <param name="translateRequests">Translation request</param>
    /// <returns></returns>
    public Dictionary<TCodesEnum, LocalizedResult> Translate(Languages language, params TranslateRequest[] translateRequests)
    {
        var results = new Dictionary<TCodesEnum, LocalizedResult>();

        foreach (var request in translateRequests)
        {
            var code = (TCodesEnum)request.CodesEnum;
            var translated = Translate(language, code, request.Replacements);
            results.TryAdd(code, translated);
        }

        return results;
    }

    /// <summary>
    /// Translates the array of requests to it's own language
    /// </summary>
    /// <param name="translateRequests"></param>
    /// <returns></returns>
    public async Task<Dictionary<TCodesEnum, LocalizedResult>> Translate(params TranslateRequest[] translateRequests)
    {
        var results = new Dictionary<TCodesEnum, LocalizedResult>();

        foreach (var request in translateRequests)
        {
            var code = (TCodesEnum)request.CodesEnum;
            var translated = await Translate(code, request.Replacements);
            results.TryAdd(code, translated);
        }

        return results;
    }
}