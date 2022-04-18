namespace DigitalSkynet.DotnetCore.Helpers.DateTimes;

/// <summary>
/// Injectable DateTime service.
/// </summary>
public interface IDateTimeService
{
    /// <summary>
    /// Gets the UTC now time
    /// </summary>
    /// <value></value>
    DateTime UtcNow { get; }
}
