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
        return Ok(_service.GetAll());
    }
    
    [HttpGet(Name = "GetDriverState")]
    public ActionResult<DriverStateDto> Get(int address)
    {
        return Ok(_service.Get(address));
    }
    
        
    [HttpPost(Name = "CreateDriverState")]
    public ActionResult<DriverStateDto> Create([FromBody]DriverStateDto driverState)
    {
        return Ok(_service.Create(driverState));
    }
    
    [HttpPatch(Name = "UpdateDriverState")]
    public ActionResult<DriverStateDto> Update([FromBody]DriverStateDto driverState)
    {
        return Ok(_service.Update(driverState));
    }
}