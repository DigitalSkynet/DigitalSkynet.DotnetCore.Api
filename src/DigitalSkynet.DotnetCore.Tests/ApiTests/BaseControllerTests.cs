using System.Security.Claims;
using DigitalSkynet.DotnetCore.Api.Controllers;
using DigitalSkynet.DotnetCore.DataStructures.Models.Response;
using DigitalSkynet.DotnetCore.Tests.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace DigitalSkynet.DotnetCore.Tests.ApiTests;

public class BaseControllerTests : BaseTest
{
    [Fact]
    public async Task BaseServiceController_HasServiceAndResponseModelWorks_WorksAsExpected()
    {
        var id = Guid.NewGuid();
        var serviceObj = new object();
        var controller = new DummyController(serviceObj);
        controller.ControllerContext = GetContext(id);

        var responeModel = await controller.GetResponseAsync();


        Assert.IsType<ActionResult<ApiResponseEnvelope<int>>>(responeModel);

        // Assert.Equal(data, responeModel.Value.Data);
        // Assert.Equal(ResponseTypes.Success, responeModel.Value.Status);

        Assert.Equal(id, controller.UserId);
        controller.AssertHasService(serviceObj);
    }

    [Fact]
    public void BaseController_TypeIntHasUser_UserIdEqualsExpectedWithCorrectType()
    {
        int id = 0;
        var controller = new BaseController<int>();
        controller.ControllerContext = GetContext(id);
        Assert.Equal(id, controller.UserId);
    }

    private ControllerContext GetContext<TUserKey>(TUserKey key) where TUserKey : struct
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                   {
            new Claim(ClaimTypes.Name, "example name"),
            new Claim(ClaimTypes.NameIdentifier, key.ToString()),
                   }, "mock"));

        return new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };
    }
}