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

    [HttpGet(Name = "alarm")]
    public ActionResult<AlarmDTO> Get(string id)
    {
       AlarmDTO alarm = _alarmService.Get(id);
       return Ok(alarm);
    }
    
    [HttpGet(Name = "alarms")]
    public ActionResult<AlarmDTO> GetAll()
    {
        IEnumerable<AlarmDTO> alarms = _alarmService.GetAll();
        return Ok(alarms);
    }
    
    [HttpPost(Name = "create")]
    public ActionResult<AlarmDTO> Create([FromBody]AlarmCreateUpdateDTO updateDto)
    {
        AlarmDTO alarm = _alarmService.Create(updateDto);
        return Ok(alarm);
    }
    
    [HttpDelete(Name = "delete")]
    public ActionResult<Boolean> Delete(string id)
    {
        return Ok(_alarmService.Delete(id));
    }
    
    [HttpPatch(Name = "update")]

    public ActionResult<Boolean> Update([FromBody]AlarmCreateUpdateDTO dto, string id)
    {
        return Ok(_alarmService.Update(dto, id));
    }

}