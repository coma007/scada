using Microsoft.AspNetCore.Mvc;

namespace scada_back.Infrastructure.Feature.TagHistory;

[ApiController]
[Route("Api/[controller]/[action]")]
public class TagHistoryController : ControllerBase
{
    private readonly ITagHistoryService _service;
    private readonly ILogger<TagHistoryController> _logger;

    public TagHistoryController(ITagHistoryService service, ILogger<TagHistoryController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet(Name = "GetAllTagHistoryRecords")]
    public ActionResult<IEnumerable<TagHistoryRecordDto>> GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet(Name = "GetAllTagHistoryRecordsByTagName")]
    public ActionResult<IEnumerable<TagHistoryRecordDto>> GetAllByTagName(string tagName)
    {
        return Ok(_service.GetAll(tagName));
    }

    [HttpGet(Name = "GetAllTagHistoryRecordsByTimeInterval")]
    public ActionResult<TagHistoryRecordDto> GetBetween(DateTime startDateTime, DateTime endDateTime)
    {
        return Ok(_service.GetBetween(startDateTime, endDateTime));
    }
    
    [HttpGet(Name = "GetLastValueBySignalType")]
    public ActionResult<TagHistoryRecordDto> GetLast(string signalType)
    {
        return Ok(_service.GetLast(signalType));
    }

    [HttpPost(Name = "CreateTagHistoryRecord")]
    public ActionResult<TagHistoryRecordDto> Create([FromBody] TagHistoryRecordDto tagRecord)
    {
        _service.Create(tagRecord);
        return Ok();
    }

}