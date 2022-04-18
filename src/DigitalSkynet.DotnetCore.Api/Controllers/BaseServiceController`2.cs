namespace DigitalSkynet.DotnetCore.Api.Controllers;

/// <summary>
/// Class. Represents base api controller with a generic service instance
/// </summary>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TUserKey"></typeparam>
public class BaseServiceController<TService, TUserKey> : BaseController<TUserKey>
where TUserKey : struct
{
    /// <summary>
    /// ctor. Initializes the controller
    /// </summary>
    /// <param name="service"></param>
    /// <returns></returns>
    protected BaseServiceController(TService service) : base()
    {
        _service = service;
    }

    /// <summary>
    /// The service
    /// </summary>
    protected TService _service;
}
