namespace DigitalSkynet.DotnetCore.Helpers.DateTimes;

/// <summary>
/// Class. Represents Date and Time Service
/// </summary>
public class DateTimeService : IDateTimeService
{
    /// <summary>
    /// Gets the UTC Now Time
    /// </summary>
    public DateTime UtcNow => DateTime.UtcNow;
}
