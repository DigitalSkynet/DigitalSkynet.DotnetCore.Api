namespace DigitalSkynet.DotnetCore.DataStructures.Models.Response;

/// <summary>
/// Class. Represents the collection response envelope
/// </summary>
/// <typeparam name="TItem">Type of the item in the collection</typeparam>
public class ApiCollectionResponseEnvelope<TItem> : ApiResponseEnvelope<List<TItem>>
{
    /// <summary>
    /// ctor. Initializes the envelope with the data
    /// </summary>
    /// <param name="data"></param>
    public ApiCollectionResponseEnvelope(List<TItem> data)
        : base(data)
    {
    }

    /// <summary>
    /// Gets the count of items in the collection
    /// </summary>
    public int TotalReturned => Data?.Count ?? 0;
}
