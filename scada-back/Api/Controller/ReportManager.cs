using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scada_back.Infrastructure.Feature.AlarmHistory;
using scada_back.Infrastructure.Feature.TagHistory;

namespace scada_back.Api.Controller;

[ApiController]
[Route("Api/[controller]/[action]")]
[Authorize]
public class ReportManager : ControllerBase
{
    private readonly IAlarmHistoryService _alarmHistoryService;
    private readonly ITagHistoryService _tagHistoryService;
    private readonly ILogger<DatabaseManager> _logger;

    public ReportManager(IAlarmHistoryService alarmHistoryService, ITagHistoryService tagHistoryService, ILogger<DatabaseManager> logger)
    {
        _alarmHistoryService = alarmHistoryService;
        _tagHistoryService = tagHistoryService;
        _logger = logger;
    }

    [HttpGet(Name = "GetAlarmRecordsBetween")]
    public ActionResult<IEnumerable<AlarmHistoryRecordDto>> GetAlarmsBetween(DateTime start, DateTime end)
    {
        IEnumerable<AlarmHistoryRecordDto> records = _alarmHistoryService.GetBetween(start, end);
        var result = Ok(records);
        _logger.LogInformation("Successfully got alarm records between");
        return result;
    }

    [HttpGet(Name = "GetAlarmRecordsByPriority")]
    public ActionResult<IEnumerable<AlarmHistoryRecordDto>> GetAlarmsByPriority(int priority)
    {
        IEnumerable<AlarmHistoryRecordDto> records = _alarmHistoryService.Get(priority);
        var result = Ok(records);
        _logger.LogInformation("Successfully got alarms by priority");
        return result;
    }

    [HttpGet(Name = "GetAllTagHistoryRecordsByTimeInterval")]
    public ActionResult<TagHistoryRecordDto> GetTagValuesBetween(DateTime startDateTime, DateTime endDateTime)
    {
        var result = Ok(_tagHistoryService.GetBetween(startDateTime, endDateTime));
        _logger.LogInformation("Successfully got tag values between");
        return result;
    }

    [HttpGet(Name = "GetLastValueBySignalType")]
    public ActionResult<TagHistoryRecordDto> GetLastTagValues(string signalType)
    {
        var result = Ok(_tagHistoryService.GetLatest(signalType));
        _logger.LogInformation("Successfully got tag last value");
        return result;
    }

    [HttpGet(Name = "GetAllTagHistoryRecordsByTagName")]
    public ActionResult<IEnumerable<TagHistoryRecordDto>> GetAllTagValuesByTagName(string tagName)
    {
        var result = Ok(_tagHistoryService.GetAll(tagName));
        _logger.LogInformation("Successfully got all tag values between");
        return result;
    }
}