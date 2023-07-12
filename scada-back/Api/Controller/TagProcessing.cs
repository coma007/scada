using Microsoft.AspNetCore.Mvc;
using scada_back.Api.ApiKey;
using scada_back.Infrastructure.Feature.DriverState;
using scada_back.Infrastructure.Feature.Tag;
using scada_back.Infrastructure.Feature.Tag.Model.Abstraction;
using scada_back.Infrastructure.Feature.TagHistory;

namespace scada_back.Api.Controller;


[ApiController]
[Route("Api/[controller]/[action]")]
[RequireApiKey]
public class TagProcessing : ControllerBase
{
    private readonly IDriverStateService _driverStateService;
    private readonly ITagService _tagService;
    private readonly ITagHistoryService _tagHistoryService;
    private readonly ILogger<DatabaseManager> _logger;

    public TagProcessing(IDriverStateService driverStateService, ITagService tagService, ITagHistoryService tagHistoryService, ILogger<DatabaseManager> logger)
    {
        _driverStateService = driverStateService;
        _tagService = tagService;
        _tagHistoryService = tagHistoryService;
        _logger = logger;
    }

    [HttpPost(Name = "CreateDriverState")]
    public ActionResult<DriverStateDto> CreateDriverState([FromBody] DriverStateDto driverState)
    {
        return Ok(_driverStateService.Create(driverState));
    }

    [HttpPatch(Name = "UpdateDriverState")]
    public ActionResult<DriverStateDto> UpdateDriverState([FromBody] DriverStateDto driverState)
    {
        return Ok(_driverStateService.Update(driverState));
    }
    
    [HttpPatch(Name = "UpdateDriverStates")]
    public ActionResult<DriverStateDto> UpdateDriverStates([FromBody] DriverStatesDto driverStates)
    {
        return Ok(_driverStateService.Update(driverStates));
    }

    [HttpGet(Name = "GetDriverState")]
    public ActionResult<DriverStateDto> GetDriverState(int address)
    {
        return Ok(_driverStateService.Get(address));
    }

   
    [HttpGet(Name = "GetAllTagsByType")]
    public ActionResult<IEnumerable<TagDto>> GetAllTagsByType(string tagType)
    {
        return Ok(_tagService.GetAll(tagType));
    }

    
    [HttpPost(Name = "CreateTagHistoryRecord")]
    public ActionResult CreateTagRecord([FromBody] TagHistoryRecordDto tagRecord)
    {
        _tagHistoryService.Create(tagRecord);
        return Ok();
    }
}