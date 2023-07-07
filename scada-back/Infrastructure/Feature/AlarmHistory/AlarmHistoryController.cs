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
    
    [HttpGet(Name = "GetAlarmRecordByPriority")]
    public ActionResult<IEnumerable<AlarmHistoryRecordDto>> GetByAlarmPriority(int priority)
    {
        IEnumerable<AlarmHistoryRecordDto> records = _alarmHistoryService.Get(priority);
        return Ok(records);
    }
    
    [HttpGet(Name = "GetAlarmRecordBetween")]
    public ActionResult<IEnumerable<AlarmHistoryRecordDto>> GetBetween(DateTime start, DateTime end)
    {
        IEnumerable<AlarmHistoryRecordDto> records = _alarmHistoryService.GetBetween(start, end);
        return Ok(records);
    }

    [HttpPost(Name = "CreateAlarmRecord")]
    public ActionResult<AlarmHistoryRecordDto> Create([FromBody]AlarmHistoryRecordDto alarmRecord)
    {
        _alarmHistoryService.Create(alarmRecord);
        return Ok();
    }
}