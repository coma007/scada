using Microsoft.AspNetCore.Mvc;
using scada_back.Api.ApiKey;

namespace scada_back.Infrastructure.Feature.TagHistory;

[ApiController]
[Route("Api/[controller]/[action]")]
[RequireApiKey]
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
}