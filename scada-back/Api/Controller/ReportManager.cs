﻿using Microsoft.AspNetCore.Mvc;
using scada_back.Infrastructure.Feature.AlarmHistory;
using scada_back.Infrastructure.Feature.TagHistory;

namespace scada_back.Api.Controller;

[ApiController]
[Route("Api/[controller]/[action]")]
public class ReportManager : ControllerBase
{
    private readonly IAlarmHistoryService _alarmHistoryService;
    private readonly ITagHistoryService _tagHistoryService;

    public ReportManager(IAlarmHistoryService alarmHistoryService, ITagHistoryService tagHistoryService)
    {
        _alarmHistoryService = alarmHistoryService;
        _tagHistoryService = tagHistoryService;
    }

    [HttpGet(Name = "GetAlarmRecordsBetween")]
    public ActionResult<IEnumerable<AlarmHistoryRecordDto>> GetAlarmsBetween(DateTime start, DateTime end)
    {
        IEnumerable<AlarmHistoryRecordDto> records = _alarmHistoryService.GetBetween(start, end);
        return Ok(records);
    }

    [HttpGet(Name = "GetAlarmRecordsByPriority")]
    public ActionResult<IEnumerable<AlarmHistoryRecordDto>> GetAlarmsByPriority(int priority)
    {
        IEnumerable<AlarmHistoryRecordDto> records = _alarmHistoryService.Get(priority);
        return Ok(records);
    }

    [HttpGet(Name = "GetAllTagHistoryRecordsByTimeInterval")]
    public ActionResult<TagHistoryRecordDto> GetTagValuesBetween(DateTime startDateTime, DateTime endDateTime)
    {
        return Ok(_tagHistoryService.GetBetween(startDateTime, endDateTime));
    }

    [HttpGet(Name = "GetLastValueBySignalType")]
    public ActionResult<TagHistoryRecordDto> GetLastTagValues(string signalType)
    {
        return Ok(_tagHistoryService.GetLast(signalType));
    }

    [HttpGet(Name = "GetAllTagHistoryRecordsByTagName")]
    public ActionResult<IEnumerable<TagHistoryRecordDto>> GetAllTagValuesByTagName(string tagName)
    {
        return Ok(_tagHistoryService.GetAll(tagName));
    }
}