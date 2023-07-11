using Microsoft.AspNetCore.Mvc;

namespace scada_back.Infrastructure.Feature.Alarm;

[ApiController]
[Route("Api/[controller]/[action]")]
public class AlarmController : ControllerBase
{
    private readonly IAlarmService _service;
    private readonly ILogger<AlarmController> _logger;
    
    public AlarmController(IAlarmService service, ILogger<AlarmController> logger)
    {
        _service = service;
        _logger = logger;
    }
    
    [HttpGet(Name = "GetAllAlarms")]
    public ActionResult<AlarmDto> GetAll()
    {
        return Ok(_service.GetAll());
    }

}