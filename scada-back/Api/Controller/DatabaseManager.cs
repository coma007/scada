using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scada_back.Api.WebSocket;
using scada_back.Infrastructure.Feature.Alarm;
using scada_back.Infrastructure.Feature.Tag;
using scada_back.Infrastructure.Feature.Tag.Model.Abstraction;
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
    private readonly ILogger<DatabaseManager> _logger;
    private readonly IWebSocketServer _webSocketServer;

    public DatabaseManager(IUserService userService, ITagService tagService, IAlarmService alarmService, IWebSocketServer webSocketServer, ILogger<DatabaseManager> logger)
    {
        _userService = userService;
        _tagService = tagService;
        _alarmService = alarmService;
        _webSocketServer = webSocketServer;
        _logger = logger;
    }

    [HttpPost(Name = "Login"), AllowAnonymous]
    public ActionResult<string> Login(LoginDto credentials)
    {
        return Ok(_userService.Login(credentials.Username, credentials.Password));
    }
    
    [HttpGet(Name = "GetTagByName")]
    public ActionResult<TagDto> GetTag(string tagName)
    {
        return Ok(_tagService.Get(tagName));
    }
    
    [HttpPost(Name = "CreateTag")]
    public ActionResult<TagDto> CreateTag([FromBody]TagDto tag)
    {
        tag = _tagService.Create(tag);
        if (tag.TagType.Contains("_input")) _webSocketServer.NotifyClientAboutNewTag(tag);
        // WebSocketHandler handler = new WebSocketHandler();
        // handler.SendMessage("NewTag", tag);
        return Ok(tag);
    }
    
    [HttpDelete(Name = "DeleteTag")]
    public ActionResult<TagDto> DeleteTag(string tagName)
    {
        return Ok(_tagService.Delete(tagName));
    }
    
    [HttpPatch(Name = "UpdateTagScan")]
    public ActionResult<TagDto> UpdateTagScan(string tagName)
    {
        TagDto updatedTag = _tagService.UpdateScan(tagName);
        _webSocketServer.NotifyClientAboutTagScan(updatedTag);
        return Ok(updatedTag);
    }
    
    [HttpPatch(Name = "UpdateOutputTagValue")]
    public ActionResult<TagDto> UpdateTagOutputValue(string tagName, double value)
    {
        return Ok(_tagService.UpdateOutputValue(tagName, value));
    }
    
    [HttpPost(Name = "CreateAlarm")]
    public ActionResult<AlarmDto> CreateAlarm([FromBody]AlarmDto alarm)
    {
        return Ok(_alarmService.Create(alarm));
    }
    
    [HttpDelete(Name = "DeleteAlarm")]
    public ActionResult<AlarmDto> DeleteAlarm(string alarmName)
    {
        return Ok(_alarmService.Delete(alarmName));
    }
    
    [HttpPatch(Name = "UpdateAlarm")]
    public ActionResult<AlarmDto> UpdateAlarm([FromBody]AlarmDto alarm)
    {
        return Ok(_alarmService.Update(alarm));
    }

}