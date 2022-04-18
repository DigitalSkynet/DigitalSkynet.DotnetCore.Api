using System.Dynamic;
using DigitalSkynet.DotnetCore.Helpers.Razor;
using DigitalSkynet.DotnetCore.Tests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DigitalSkynet.DotnetCore.Tests.Razor
{
    public class RazorTests : BaseTest
    {

        private readonly IRazorHelper _razorHelper;

        private const string Model = "I'm a string passed to view as a model";
        private const string ViewBagString = "I'm a string passed to view as a model";


        public RazorTests()
        {
            _razorHelper = ServiceProvider.GetRequiredService<IRazorHelper>();
        }

        [Fact]
        public async Task TestViewRender_ExistingView_TextMatches()
        {
            var result = await _razorHelper.RenderViewToStringAsync("Test.cshtml", Model);
            Assert.Equal(Model, result);
        }

        [Fact]
        public async Task TestViewRenderWithoutModel_ExistingView_TextMatches()
        {
            var result = await _razorHelper.RenderViewToStringAsync("TestWithoutModel.cshtml");
            Assert.Equal("I am a view rendered using razor engine", result);
        }

        [Fact]
        public async Task TestViewRenderWithViewBag_ExistingView_TextMatches()
        {
            dynamic viewBag = new ExpandoObject();
            viewBag.viewBagString = ViewBagString;

            var result = await _razorHelper.RenderViewToStringAsync("TestWithViewBag.cshtml", Model, viewBag);
            Assert.Equal($"{Model} {ViewBagString}", result);
        }
    }
}