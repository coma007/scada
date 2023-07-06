using Microsoft.AspNetCore.Mvc;

namespace scada_back.Alarm;

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
    
    [HttpGet(Name = "GetAlarmByName")]
    public ActionResult<AlarmDto> Get(string alarmName)
    {
        return Ok(_service.Get(alarmName));
    }

    [HttpPost(Name = "CreateAlarm")]
    public ActionResult<AlarmDto> Create([FromBody]AlarmDto alarm)
    {
        return Ok(_service.Create(alarm));
    }
    
    [HttpDelete(Name = "DeleteAlarm")]
    public ActionResult<AlarmDto> Delete(string alarmName)
    {
        return Ok(_service.Delete(alarmName));
    }
    
    [HttpPatch(Name = "UpdateAlarm")]
    public ActionResult<AlarmDto> Update([FromBody]AlarmDto alarm)
    {
        return Ok(_service.Update(alarm));
    }

}