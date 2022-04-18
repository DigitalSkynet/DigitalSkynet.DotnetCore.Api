using DigitalSkynet.DotnetCore.DataStructures.Localization;
using DigitalSkynet.DotnetCore.Helpers.Localization;
using Microsoft.Extensions.Localization;

namespace DigitalSkynet.DotnetCore.Tests.Localization;

public class GermanDefaultTestTransator : SingleLanguageTranslator<TestLocalizationCodes>
{
    public GermanDefaultTestTransator(IStringLocalizerFactory factory) : base(Languages.German, factory)
    { }
}