namespace DigitalSkynet.DotnetCore.Api.Controllers
{
    public class BaseServiceController<TService> : BaseController
    {
        protected BaseServiceController(TService service) : base()
        {
            _service = service;
        }
        protected TService _service;
    }
}
