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
        return Ok(records);
    }
    
    [HttpGet(Name = "GetAlarmRecordByName")]
    public ActionResult<IEnumerable<AlarmHistoryRecordDto>> GetByAlarmName(string alarmName)
    {
        IEnumerable<AlarmHistoryRecordDto> records = _alarmHistoryService.Get(alarmName);
        return Ok(records);
    }

    [HttpPost(Name = "CreateAlarmRecord")]
    public ActionResult<AlarmHistoryRecordDto> Create([FromBody]AlarmHistoryRecordDto alarmRecord)
    {
        _alarmHistoryService.Create(alarmRecord);
        return Ok();
    }
}