using System.Text;

namespace DigitalSkynet.DotnetCore.Helpers.Strings;

/// <summary>
/// Class. Represents string extensions
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Encodes non-ascii characters
    /// </summary>
    /// <param name="value">Source string</param>
    /// <returns>Encoded string</returns>
    public static string EncodeNonAsciiCharacters(this string value)
    {
        const sbyte asciiMaxIndex = 127;

        var sb = new StringBuilder();
        foreach (var c in value)
        {
            if (c > asciiMaxIndex)
            {
                var encodedValue = $@"\u{(int)c:x4}";
                sb.Append(encodedValue);
            }
            else
            {
                sb.Append(c);
            }
        }

        return sb.ToString();
    }
}