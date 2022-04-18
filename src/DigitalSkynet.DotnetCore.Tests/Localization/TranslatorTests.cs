using DigitalSkynet.DotnetCore.DataStructures.Localization;
using DigitalSkynet.DotnetCore.Tests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DigitalSkynet.DotnetCore.Tests.Localization;

public class TranslatorTests : BaseTest
{
    private readonly EnglishDefaultTestTranslator _service;
    private readonly GermanDefaultTestTransator _germanTranslator;
    public TranslatorTests()
    {
        _service = ServiceProvider.GetRequiredService<EnglishDefaultTestTranslator>();
        _germanTranslator = ServiceProvider.GetRequiredService<GermanDefaultTestTransator>();
    }

    [Fact]
    public async Task TestTranslationToGerman_DefaultEnglishTranslatorGetSingleTranslation_TranslationMatchesExpected()
    {
        string replacement = DateTime.Now.TimeOfDay.ToString();
        var result = await _germanTranslator.Translate(TestLocalizationCodes.TestString, replacement);
        Assert.Equal(_germanTranslator.CurrentLanguage, result.LanguageInfo.Language);
        Assert.Equal(_germanTranslator.CurrentLanguage.ToString(), result.LanguageInfo.Name);
        Assert.Equal((int)_germanTranslator.CurrentLanguage, result.LanguageInfo.Id);
        Assert.Equal($"Dies ist eine lokalisierte Testzeichenfolge, Parameter \\u00fcbergeben {replacement}", result.Result);
        Assert.Equal(_germanTranslator.CurrentLanguage, result.LanguageInfo.Language);
    }

    [Fact]
    public async Task TestTranslation_DefaultEnglishTranslatorGetSingleTranslation_TranslationMatchesExpected()
    {
        string replacement = DateTime.Now.TimeOfDay.ToString();
        var result = await _service.Translate(TestLocalizationCodes.TestString, replacement);
        Assert.Equal($"This is test localized string, parameter passed {replacement}", result.Result);
        Assert.Equal(_service.CurrentLanguage, result.LanguageInfo.Language);
    }

    [Fact]
    public async Task TestTranslation_DefaultEnglishTranslatorGetBulkTranslation_TranslationMatchesExpected()
    {
        string replacement = DateTime.Now.TimeOfDay.ToString();

        var code = TestLocalizationCodes.TestString;
        var result = await _service.Translate(new TranslateRequest(code, replacement));

        Assert.True(result.ContainsKey(code));
        var testTranslation = result[code];
        Assert.NotNull(testTranslation);
        Assert.Equal($"This is test localized string, parameter passed {replacement}", testTranslation.Result);
        Assert.Equal(_service.CurrentLanguage, testTranslation.LanguageInfo.Language);
    }
}