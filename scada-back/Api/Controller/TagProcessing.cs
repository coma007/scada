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
        var result = Ok(_driverStateService.Create(driverState));
        _logger.LogInformation("Successfully created driver state");
        return result;
    }

    [HttpPatch(Name = "UpdateDriverState")]
    public ActionResult<DriverStateDto> UpdateDriverState([FromBody] DriverStateDto driverState)
    {
        var result = Ok(_driverStateService.Update(driverState));
        _logger.LogInformation("Successfully updated driver state");
        return result;
    }
    
    [HttpPatch(Name = "UpdateDriverStates")]
    public ActionResult<DriverStateDto> UpdateDriverStates([FromBody] DriverStatesDto driverStates)
    {
        var result = Ok(_driverStateService.Update(driverStates));
        _logger.LogInformation("Successfully updated driver states");
        return result;
    }

    [HttpGet(Name = "GetDriverState")]
    public ActionResult<DriverStateDto> GetDriverState(int address)
    {
        var result = Ok(_driverStateService.Get(address));
        _logger.LogInformation("Successfully got driver state");
        return result;
    }

   
    [HttpGet(Name = "GetAllTagsByType")]
    public ActionResult<IEnumerable<TagDto>> GetAllTagsByType(string tagType)
    {
        var result = Ok(_tagService.GetAll(tagType));
        _logger.LogInformation("Successfully got tags by type");
        return result;
    }

    
    [HttpPost(Name = "CreateTagHistoryRecord")]
    public ActionResult CreateTagRecord([FromBody] TagHistoryRecordDto tagRecord)
    {
        _tagHistoryService.Create(tagRecord);
        var result = Ok();
        _logger.LogInformation("Successfully created tag history record");
        return result;
    }
}