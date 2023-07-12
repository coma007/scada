using Microsoft.AspNetCore.Mvc;

namespace scada_back.Infrastructure.Feature.AlarmHistory;


[ApiController]
[Route("Api/[controller]/[action]")]
public class AlarmHistoryController: ControllerBase
{
    private readonly IAlarmHistoryService _alarmHistoryService;
    private readonly ILogger<AlarmHistoryController> _logger;
    
    public AlarmHistoryController(IAlarmHistoryService alarmHistoryService, ILogger<AlarmHistoryController> logger)
    {
        _alarmHistoryService = alarmHistoryService;
        _logger = logger;
    }
    
    [HttpGet(Name = "GetAllAlarmRecords")]
    public ActionResult<IEnumerable<AlarmHistoryRecordDto>> GetAll()
    {
        IEnumerable<AlarmHistoryRecordDto> records = _alarmHistoryService.GetAll();
        var result =  Ok(records);
        _logger.LogInformation("Successfully got alarms history");
        return result;
    }
    
    [HttpGet(Name = "GetAlarmRecordByName")]
    public ActionResult<IEnumerable<AlarmHistoryRecordDto>> GetByAlarmName(string alarmName)
    {
        IEnumerable<AlarmHistoryRecordDto> records = _alarmHistoryService.Get(alarmName);
        var result = Ok(records);
        _logger.LogInformation("Successfully got alarms by name");
        return result;
    }

    [HttpPost(Name = "CreateAlarmRecord")]
    public ActionResult<AlarmHistoryRecordDto> Create([FromBody]AlarmHistoryRecordDto alarmRecord)
    {
        _alarmHistoryService.Create(alarmRecord);
        _logger.LogInformation("Successfully created alarm history");
        return Ok();
    }
}