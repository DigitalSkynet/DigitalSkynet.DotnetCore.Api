namespace DigitalSkynet.DotnetCore.DataStructures.Interfaces;

/// <summary>
/// Interface. Describes the object with supports soft deletion.
/// E.G. Setting IsDeleted field to true or false
/// </summary>
public interface ISoftDeletable
{
    /// <summary>
    /// Gets or sets the value which indicates that the entity is deleted
    /// </summary>
    /// <value></value>
    bool IsDeleted { get; set; }
}
