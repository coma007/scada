using Microsoft.AspNetCore.Mvc;

namespace scada_back.Alarm;

[ApiController]
[Route("Api/[controller]/[action]")]
public class AlarmController : ControllerBase
{
    private readonly IAlarmService _alarmService;
    private readonly ILogger<AlarmController> _logger;
    
    public AlarmController(IAlarmService alarmService, ILogger<AlarmController> logger)
    {
        _alarmService = alarmService;
        _logger = logger;
    }
    [HttpGet(Name = "GetAllAlarms")]
    public ActionResult<AlarmDto> GetAll()
    {
        return Ok(_alarmService.GetAll());
    }
    
    [HttpGet(Name = "GetAlarmByName")]
    public ActionResult<AlarmDto> Get(string alarmName)
    {
        return Ok(_alarmService.Get(alarmName));
    }

    
    [HttpPost(Name = "CreateAlarm")]
    public ActionResult<AlarmDto> Create([FromBody]AlarmDto alarm)
    {
        return Ok(_alarmService.Create(alarm));
    }
    
    [HttpDelete(Name = "DeleteAlarm")]
    public ActionResult<AlarmDto> Delete(string alarmName)
    {
        return Ok(_alarmService.Delete(alarmName));
    }
    
    [HttpPatch(Name = "UpdateAlarm")]
    public ActionResult<AlarmDto> Update([FromBody]AlarmDto alarm)
    {
        return Ok(_alarmService.Update(alarm));
    }

}