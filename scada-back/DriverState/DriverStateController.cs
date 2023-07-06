using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace scada_back.DriverState;

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
    public ActionResult<DriverStateDTO> GetAll()
    {
        return Ok(_service.GetAll());
    }
    
    [HttpGet(Name = "GetDriverState")]
    public ActionResult<DriverStateDTO> Get(int address)
    {
        return Ok(_service.Get(address));
    }
    
        
    [HttpPost(Name = "CreateDriverState")]
    public ActionResult<DriverStateDTO> Create([FromBody]DriverStateDTO dto)
    {
        return Ok(_service.Create(dto));
    }
    
    [HttpPatch(Name = "UpdateDriverState")]
    public ActionResult<DriverStateDTO> Update([FromBody]DriverStateDTO dto)
    {
        return Ok(_service.Update(dto));
    }
}