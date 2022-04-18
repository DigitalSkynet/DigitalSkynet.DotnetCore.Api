using DigitalSkynet.DotnetCore.DataStructures.Enums.Api;

namespace DigitalSkynet.DotnetCore.DataStructures.Models.Paging;

/// <summary>
/// Class. Represetns sorting instructions
/// </summary>
public class SortModel
{
    /// <summary>
    /// Gets or sets the filed name to be sorted by
    /// </summary>
    /// <value></value>
    public string FieldName { get; set; } // short names are more suitable for GET requests

    /// <summary>
    /// Gets or sets the sortding direction
    /// </summary>
    /// <value></value>
    public SortDirections Direction { get; set; }
}