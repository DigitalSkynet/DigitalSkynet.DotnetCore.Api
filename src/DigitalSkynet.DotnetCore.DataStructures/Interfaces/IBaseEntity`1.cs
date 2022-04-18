namespace DigitalSkynet.DotnetCore.DataStructures.Interfaces;

/// <summary>
/// Interface. Describes the object with the Id property of given type
/// </summary>
/// <typeparam name="TKey">The type of id property</typeparam>
public interface IHasKey<TKey>
    where TKey : struct
{
    /// <summary>
    /// Gets or sets the id of the entity
    /// </summary>
    /// <value></value>
    TKey Id { get; set; }
}
