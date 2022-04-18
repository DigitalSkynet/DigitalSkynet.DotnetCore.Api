using System;

namespace DigitalSkynet.DotnetCore.DataStructures.Interfaces;

/// <summary>
/// Interface. Describes the object which has created and updated dates
/// </summary>
public interface ITimestamped
{
    /// <summary>
    /// Gets or sets created date
    /// </summary>
    /// <value></value>
    DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets updated date
    /// </summary>
    /// <value></value>
    DateTime UpdatedDate { get; set; }
}
