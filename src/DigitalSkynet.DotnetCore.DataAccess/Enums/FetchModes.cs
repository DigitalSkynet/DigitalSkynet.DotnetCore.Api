namespace DigitalSkynet.DotnetCore.DataAccess.Enums;

/// <summary>
/// Enum. Used to change repository fetch behavior
/// </summary>
[Flags]
public enum FetchModes
{
    /// <summary>
    /// Default behavior returns non-deleted and no tracking
    /// </summary>
    NoTracking = 0,
    /// <summary>
    /// Fetches data with no tracking
    /// </summary>
    Tracking = 1,
    /// <summary>
    /// Fetches deleted data
    /// </summary>
    FindDeleted = 2
}