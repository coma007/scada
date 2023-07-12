using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scada_back.Api.WebSocket;
using scada_back.Infrastructure.Feature.Alarm;
using scada_back.Infrastructure.Feature.Tag;
using scada_back.Infrastructure.Feature.Tag.Model.Abstraction;
using scada_back.Infrastructure.Feature.TagHistory;
using scada_back.Infrastructure.Feature.User;

namespace scada_back.Api.Controller;

[ApiController]
[Route("Api/[controller]/[action]")]
[Authorize]
public class DatabaseManager : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITagService _tagService;
    private readonly IAlarmService _alarmService;
    private readonly ITagHistoryService _tagHistoryService;
    private readonly IWebSocketServer _webSocketServer;
    private readonly ILogger<DatabaseManager> _logger;

    public DatabaseManager(IUserService userService, ITagService tagService, IAlarmService alarmService,
        ITagHistoryService tagHistoryService, IWebSocketServer webSocketServer, ILogger<DatabaseManager> logger)
    {
        _userService = userService;
        _tagService = tagService;
        _tagHistoryService = tagHistoryService;
        _alarmService = alarmService;
        _webSocketServer = webSocketServer;
        _logger = logger;
    }

    [HttpPost(Name = "Login"), AllowAnonymous]
    public ActionResult<string> Login(LoginDto credentials)
    {
        var result = Ok(_userService.Login(credentials.Username, credentials.Password));
        _logger.LogInformation("Successfully logged in");
        return result;
    }
    
    [HttpGet(Name = "GetTagByName")]
    public ActionResult<TagDto> GetTag(string tagName)
    {
        var result = Ok(_tagService.Get(tagName));
        _logger.LogInformation("Successfully got tag by tag name");
        return result;
    }

    [HttpGet(Name = "GetTagLastValue")]
    public ActionResult<string> GetTagLastValue(string tagName)
    {
        var result = Ok(_tagHistoryService.GetLastValueForTag(tagName));
        _logger.LogInformation("Successfully got last tag value");
        return result;
    }

    [HttpPost(Name = "CreateTag")]
    public ActionResult<TagDto> CreateTag([FromBody]TagDto tag)
    {
        tag = _tagService.Create(tag);
        if (tag.TagType.Contains("_input")) _webSocketServer.NotifyClientAboutNewTag(tag);
        var result = Ok(tag);
        _logger.LogInformation("Successfully created tag");
        return result;
    }
    
    [HttpDelete(Name = "DeleteTag")]
    public ActionResult<TagDto> DeleteTag(string tagName)
    {
        TagDto tag = _tagService.Delete(tagName);
        if (tag.TagType.Contains("_input")) _webSocketServer.NotifyClientAboutTagDelete(tag);
        var result = Ok(tag);
        _logger.LogInformation("Successfully deleted tag");
        return result;
    }
    
    [HttpPatch(Name = "UpdateTagScan")]
    public ActionResult<TagDto> UpdateTagScan(string tagName)
    {
        TagDto updatedTag = _tagService.UpdateScan(tagName);
        _webSocketServer.NotifyClientAboutTagScan(updatedTag);
        var result = Ok(updatedTag);
        _logger.LogInformation("Successfully updated tag scan");
        return result;
    }
    
    [HttpPatch(Name = "UpdateOutputTagValue")]
    public ActionResult<TagDto> UpdateTagOutputValue(string tagName, double value)
    {
        TagDto tag = _tagService.UpdateOutputValue(tagName, value);
        _tagHistoryService.Create(new TagHistoryRecordDto
        {
            TagName = tag.TagName,
            Timestamp = DateTime.Now,
            TagValue = value
        });
        var result = Ok(tag);
        _logger.LogInformation("Successfully updated output tag value");
        return result;
    }
    
    
    [HttpGet(Name = "GetAlarmByAlarmName")]
    public ActionResult<AlarmDto> GetAlarm(string alarmName)
    {
        var result = Ok(_alarmService.Get(alarmName));
        _logger.LogInformation("Successfully got alarm by name");
        return result;
    }
    
    [HttpGet(Name = "GetByTagName")]
    public ActionResult<AlarmDto> GetAlarmByTagName(string name)
    {
        var result = Ok(_alarmService.GetByTag(name));
        _logger.LogInformation("Successfully got alarm by tag name");
        return result;
    }
    
    [HttpPost(Name = "CreateAlarm")]
    public ActionResult<AlarmDto> CreateAlarm([FromBody]AlarmDto alarm)
    {
        var result = Ok(_alarmService.Create(alarm));
        _logger.LogInformation("Successfully created alarm");
        return result;
    }
    
    [HttpDelete(Name = "DeleteAlarm")]
    public ActionResult<AlarmDto> DeleteAlarm(string alarmName)
    {
        var result = Ok(_alarmService.Delete(alarmName));
        _logger.LogInformation("Successfully deleted alarm");
        return result;
    }
    
    [HttpPatch(Name = "UpdateAlarm")]
    public ActionResult<AlarmDto> UpdateAlarm([FromBody]AlarmDto alarm)
    {
        var result = Ok(_alarmService.Update(alarm));
        _logger.LogInformation("Successfully updated alarm");
        return result;
    }

}