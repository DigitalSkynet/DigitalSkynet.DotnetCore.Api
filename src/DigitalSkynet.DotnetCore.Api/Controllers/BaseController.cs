using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using DigitalSkynet.DotnetCore.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigitalSkynet.DotnetCore.Api.Controllers
{
    public class BaseController : Controller
    {
        public int UserId => int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

        public IActionResult JsonResult<T>(T result)
        {
            return Ok(result);
        }

        public IActionResult ResponseModel<T>(T result) where T : class, new()
        {
            var response = new ResponseModel<T>(result);
            return JsonResult(response);
        }

        public IActionResult PagedResponse<TSummary, TPayload>(List<TPayload> payload, TSummary summary)
        where TPayload : class, new()
        where TSummary : PayloadSummary
        {
            var result = new PagedResponseModel<TPayload, TSummary>(payload, summary);
            return JsonResult(result);
        }

        public IActionResult FileResult<T>(T result)
        {
            Response.Headers.Add("Content-Disposition", "attachment");
            return Ok(result);
        }
    }
}
