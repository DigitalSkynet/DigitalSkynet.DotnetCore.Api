namespace DigitalSkynet.DotnetCore.DataStructures.Models.Paging;

/// <summary>
/// Class. Represents the paged collection
/// </summary>
/// <typeparam name="TItem"></typeparam>
public class Paged<TItem>
{
    /// <summary>
    /// ctor. Initializes the class with the paged data
    /// </summary>
    /// <param name="data"></param>
    /// <param name="total"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    public Paged(List<TItem> data, int total, int pageNumber, int pageSize)
    {
        Data = data;
        Total = total;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    /// <summary>
    /// Gets  the data sotred in the paged result
    /// </summary>
    /// <value></value>
    public List<TItem> Data { get; }
    /// <summary>
    /// Gets the total number of items
    /// </summary>
    /// <value></value>
    public int Total { get; }
    /// <summary>
    /// Gets the page numer
    /// </summary>
    /// <value></value>
    public int PageNumber { get; }
    /// <summary>
    /// Gets the page size
    /// </summary>
    /// <value></value>
    public int PageSize { get; }
    /// <summary>
    /// Gets the total number of pages according to Total number and the Page size
    /// </summary>
    /// <returns></returns>
    public int TotalPages => (int)Math.Ceiling((double)Total / PageSize);
}