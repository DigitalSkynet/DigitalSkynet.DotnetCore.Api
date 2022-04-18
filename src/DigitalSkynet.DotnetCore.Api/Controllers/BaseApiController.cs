using DigitalSkynet.DotnetCore.DataStructures.Enums.Api;
using DigitalSkynet.DotnetCore.DataStructures.Models.Paging;
using DigitalSkynet.DotnetCore.DataStructures.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace DigitalSkynet.DotnetCore.Api.Controllers;

/// <summary>
/// A class which provides basic methods for ASP.NET Core REST API controllers.
/// </summary>
public class BaseApiController : Controller

{
    /// <summary>
    /// Wraps the model into an envelope with basic system fields.
    /// </summary>
    /// <typeparam name="T">The type of the model.</typeparam>
    /// <param name="result">The response model.</param>
    /// <returns>A typed ActionResult with JSON response.</returns>
    protected ActionResult<ApiResponseEnvelope<T>> ResponseModel<T>(T result)
    {
        return Json(new ApiResponseEnvelope<T>(result));
    }


    /// <summary>
    /// Returns Success response
    /// </summary>
    /// <returns></returns>
    protected ActionResult<ApiResponseEnvelope> ResponseOk()
    {
        return Json(new ApiResponseEnvelope(ResponseTypes.Success));
    }

    /// <summary>
    /// Wraps the collection of models into an envelope with basic system fields specific for collections.
    /// </summary>
    /// <typeparam name="TItem">The type of the elements in the collection.</typeparam>
    /// <param name="result"></param>
    /// <returns></returns>
    protected ActionResult<ApiCollectionResponseEnvelope<TItem>> CollectionResponse<TItem>(List<TItem> result)
    {
        return Json(new ApiCollectionResponseEnvelope<TItem>(result));
    }

    /// <summary>
    /// Wraps the collection of models into an envelope with basic system fields specific for a paged collections.
    /// </summary>
    /// <param name="result"></param>
    /// <param name="total"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <typeparam name="TItem"></typeparam>
    /// <returns></returns>
    protected ActionResult<ApiPagedResponseEnvelope<TItem>> PagedCollectionResponse<TItem>(List<TItem> result, int total, int pageNumber, int pageSize)
    {
        return Json(new ApiPagedResponseEnvelope<TItem>(result, total, pageNumber, pageSize));
    }

    /// <summary>
    /// Wraps the collection of models into an envelope with basic system fields specific for a paged collections.
    /// </summary>
    /// <param name="result">The paged result</param>
    /// <typeparam name="TItem"></typeparam>
    /// <returns></returns>
    protected ActionResult<ApiPagedResponseEnvelope<TItem>> PagedCollectionResponse<TItem>(Paged<TItem> result)
    {
        return Json(new ApiPagedResponseEnvelope<TItem>(result.Data, result.Total, result.PageNumber, result.PageSize));
    }

    /// <summary>
    /// Returns file
    /// </summary>
    /// <param name="result"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected IActionResult FileResult<T>(T result)
    {
        Response.Headers.Add("Content-Disposition", "attachment");
        return Ok(result);
    }
}