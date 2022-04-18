namespace DigitalSkynet.DotnetCore.DataStructures.Localization.Interfaces;

/// <summary>
/// Used to get `ILanguageDescription` instance by `Languages` enumeration type parameter.
/// </summary>
public interface ILanguageInfo
{
    /// <summary>
    /// Gets the id of the language
    /// </summary>
    /// <value></value>
    int Id { get; }

    /// <summary>
    /// Gets the human readble name of the language
    /// </summary>
    /// <value></value>
    string Name { get; }

    /// <summary>
    /// Gets the code of the language
    /// </summary>
    /// <value></value>
    string Code { get; }
}