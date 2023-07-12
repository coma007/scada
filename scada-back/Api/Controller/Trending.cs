using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scada_back.Infrastructure.Feature.TagHistory;

namespace scada_back.Api.Controller;

[ApiController]
[Route("Api/[controller]/[action]")]
[Authorize]
public class Trending : ControllerBase
{
    private readonly ITagHistoryService _tagHistoryService;
    private readonly ILogger<DatabaseManager> _logger;

    public Trending(ITagHistoryService tagHistoryService, ILogger<DatabaseManager> logger)
    {
        _tagHistoryService = tagHistoryService;
        _logger = logger;
    }
    
    [HttpGet(Name = "GetLatestInputScanValues")]
    public ActionResult<TagHistoryRecordDto> GetLatestInputScanValues()
    {
        return Ok(_tagHistoryService.GetLatestInputScan());
    }
}