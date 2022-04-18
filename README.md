# Digital Skynet web api and exception middleware implementation for dotnet core

The package contains implementation of base controllers, response payload models and exception middleware

# Visit our nuget to get all the packages
   
   https://www.nuget.org/profiles/DigitalSkynet

## Usage

```java
// Bind ILogErrorsService to your custom implementation
services.AddScoped<ILogErrorsService, MyLoggerService>();
```

```java
using DigitalSkynet.DotnetCore.Api.Controllers;

public class DashboardController : BaseServiceController<IDashboardService>
{
    // Injecting a service
    public DashboardController(IDashboardService service): base(service)
    {}

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _service.Get();
        // Returning a payload model
        return ResponseModel(result);
    }
}

```
