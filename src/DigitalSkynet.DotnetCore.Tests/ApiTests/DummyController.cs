using DigitalSkynet.DotnetCore.Api.Controllers;
using DigitalSkynet.DotnetCore.DataStructures.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace DigitalSkynet.DotnetCore.Tests.ApiTests;

public class DummyController : BaseServiceController<object, Guid>
{
    public DummyController(object service) : base(service)
    { }

    public Task<ActionResult<ApiResponseEnvelope<int>>> GetResponseAsync()
    {
        var result = ResponseModel(_service.GetHashCode());
        return Task.FromResult(result);
    }

    public void AssertHasService(object expected)
    {
        Assert.NotNull(_service);
        Assert.Equal(expected.GetHashCode(), _service.GetHashCode());
    }
}