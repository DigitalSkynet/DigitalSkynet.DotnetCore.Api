using System.ComponentModel;
using System.Security.Claims;

namespace DigitalSkynet.DotnetCore.Api.Controllers;

/// <summary>
/// A class which provides basic methods for ASP.NET Core REST API controllers.
/// </summary>
public class BaseController<TUserId> : BaseApiController
    where TUserId : struct
{
    /// <summary>
    /// A property which returns the integer value of the NameIdentifier claim associated to the current session.
    /// </summary>
    public TUserId UserId => (TUserId)TypeDescriptor.GetConverter(typeof(TUserId)).ConvertFromInvariantString(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
}
