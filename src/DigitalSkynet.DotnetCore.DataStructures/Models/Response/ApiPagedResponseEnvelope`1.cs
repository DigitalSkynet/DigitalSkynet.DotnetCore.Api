using DigitalSkynet.DotnetCore.DataStructures.Models.Paging;

namespace DigitalSkynet.DotnetCore.DataStructures.Models.Response;

/// <summary>
/// Class. Represents the envelope for paged data
/// </summary>
/// <typeparam name="TItem"></typeparam>
public class ApiPagedResponseEnvelope<TItem> : ApiCollectionResponseEnvelope<TItem>
{

    /// <summary>
    /// ctor. Initializes the envelope with the paged data
    /// </summary>
    /// <param name="paged">Paged data</param>
    /// <returns></returns>
    public ApiPagedResponseEnvelope(Paged<TItem> paged) : this(paged.Data, paged.Total, paged.PageNumber, paged.PageSize)
    { }

    /// <summary>
    /// ctor. Initializes the envelope with the paged data
    /// </summary>
    /// <param name="data">Actual data</param>
    /// <param name="total">Total items</param>
    /// <param name="pageNumber">Current page number</param>
    /// <param name="pageSize">Page size</param>
    public ApiPagedResponseEnvelope(List<TItem> data, int total, int pageNumber, int pageSize)
        : base(data)
    {
        Total = total;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    /// <summary>
    /// Gets the total number of items in the collection
    /// </summary>
    /// <value></value>
    public int Total { get; protected set; }

    /// <summary>
    /// Gets the current page number
    /// </summary>
    /// <value></value>
    public int PageNumber { get; protected set; }

    /// <summary>
    /// Gets the page size
    /// </summary>
    /// <value></value>
    public int PageSize { get; protected set; }
}
