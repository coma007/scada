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
    [Route("alarm-record")]
    public ActionResult<AlarmHistoryRecordDTO> Get(string id)
    {
        AlarmHistoryRecordDTO record = _alarmHistoryRecordService.Get(id);
        return Ok(record);
    }
    
    [HttpGet]
    [Route("alarm-records")]
    public ActionResult<IEnumerable<AlarmHistoryRecordDTO>> GetAll()
    {
        IEnumerable<AlarmHistoryRecordDTO> records = _alarmHistoryRecordService.GetAll();
        return Ok(records);
    }
    
    [HttpGet]
    [Route("alarm-records/between")]
    public ActionResult<IEnumerable<AlarmHistoryRecordDTO>> GetBetween(DateTime start, DateTime end)
    {
        IEnumerable<AlarmHistoryRecordDTO> records = _alarmHistoryRecordService.GetBetween(start, end);
        return Ok(records);
    }

    [HttpPost]
    [Route("alarm-record/create")]
    public ActionResult<AlarmDTO> Create([FromBody]AlarmHistoryRecordCreateDTO createDto)
    {
        AlarmHistoryRecordDTO record = _alarmHistoryRecordService.Create(createDto);
        return Ok(record);
    }
}