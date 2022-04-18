using System.Linq.Expressions;

namespace DigitalSkynet.DotnetCore.DataStructures.Models.Paging;

/// <summary>
/// lass. Represents the Paging Model
/// </summary>
public class PagingModel
{
    /// <summary>
    /// Gets or sets Current page number
    /// </summary>
    /// <value></value>
    public int PageNumber { get; set; }
    /// <summary>
    /// Gets or sets Current page size
    /// </summary>
    /// <value></value>
    public int PageSize { get; set; }

    /// <summary>
    /// Gets or sets sort direction
    /// </summary>
    /// <value></value>
    public List<SortModel> Sort { get; set; }

    /// <summary>
    /// Gets or sets the filter
    /// </summary>
    /// <value></value>
    public string Filter { get; set; }
}

/// <summary>
/// Class. Represents the Paging Model with a custom predicated for paging
/// </summary>
/// <typeparam name="TProjection">Type of the model</typeparam>
public abstract class PagingModel<TProjection> : PagingModel
{
    /// <summary>
    /// Gets a predicate based on Filter property.
    /// </summary>
    /// <returns></returns>
    public abstract Expression<Func<TProjection, bool>> GetPredicate();
}