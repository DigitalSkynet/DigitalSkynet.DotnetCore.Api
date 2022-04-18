using DigitalSkynet.DotnetCore.DataStructures.Localization;
using DigitalSkynet.DotnetCore.Helpers.Localization;
using Microsoft.Extensions.Localization;

namespace DigitalSkynet.DotnetCore.Tests.Localization;

public class EnglishDefaultTestTranslator : SingleLanguageTranslator<TestLocalizationCodes>
{
    public EnglishDefaultTestTranslator(IStringLocalizerFactory factory) : base(Languages.English, factory)
    { }
}