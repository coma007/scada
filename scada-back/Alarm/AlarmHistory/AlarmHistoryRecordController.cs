using Microsoft.AspNetCore.Mvc;

namespace scada_back.Alarm.AlarmHistory;


[ApiController]
[Route("Api/[controller]/[action]")]
public class AlarmHistoryRecordController: ControllerBase
{
    private readonly IAlarmHistoryRecordService _alarmHistoryRecordService;
    private readonly ILogger<AlarmHistoryRecordController> _logger;
    
    public AlarmHistoryRecordController(IAlarmHistoryRecordService alarmHistoryRecordService, ILogger<AlarmHistoryRecordController> logger)
    {
        _alarmHistoryRecordService = alarmHistoryRecordService;
        _logger = logger;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<AlarmHistoryRecordDTO>> GetAll()
    {
        IEnumerable<AlarmHistoryRecordDTO> records = _alarmHistoryRecordService.GetAll();
        return Ok(records);
    }
    
    [HttpGet(Name = "GetAlarmRecordByName")]
    public ActionResult<IEnumerable<AlarmHistoryRecordDTO>> GetByName(string name)
    {
        IEnumerable<AlarmHistoryRecordDTO> records = _alarmHistoryRecordService.GetByName(name);
        return Ok(records);
    }
    
    [HttpGet(Name = "GetAlarmRecordBetween")]
    public ActionResult<IEnumerable<AlarmHistoryRecordDTO>> GetBetween(DateTime start, DateTime end)
    {
        IEnumerable<AlarmHistoryRecordDTO> records = _alarmHistoryRecordService.GetBetween(start, end);
        return Ok(records);
    }

    [HttpPost(Name = "CreateAlarmRecord")]
    public ActionResult<AlarmHistoryRecordDTO> Create([FromBody]AlarmHistoryRecordDTO createDto)
    {
        AlarmHistoryRecordDTO record = _alarmHistoryRecordService.Create(createDto);
        return Ok(record);
    }
}