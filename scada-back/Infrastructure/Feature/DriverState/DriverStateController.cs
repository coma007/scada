using Microsoft.AspNetCore.Mvc;

namespace scada_back.Infrastructure.Feature.DriverState;

[ApiController]
[Route("Api/[controller]/[action]")]
public class DriverStateController : ControllerBase
{
    private readonly IDriverStateService _service;
    private readonly ILogger<DriverStateController> _logger;
    
    public DriverStateController(IDriverStateService service, ILogger<DriverStateController> logger)
    {
        _service = service;
        _logger = logger;
    }
    
    [HttpGet(Name = "GetAllDriverState")]
    public ActionResult<DriverStateDto> GetAll()
    {
        var result = Ok(_service.GetAll());
        _logger.LogInformation("Successfully got all driver states");
        return result;
    }
}