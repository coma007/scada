using Microsoft.AspNetCore.Mvc;
using scada_back.Infrastructure.Feature.Tag.Model.Abstraction;

namespace scada_back.Infrastructure.Feature.Tag;

[ApiController]
[Route("Api/[controller]/[action]")]
public class TagController : ControllerBase
{
    private readonly ITagService _service;
    private readonly ILogger<TagController> _logger;

    public TagController(ITagService service, ILogger<TagController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet(Name = "GetAll")]
    public ActionResult<IEnumerable<TagDto>> GetAll()
    {
        var result =  Ok(_service.GetAll());
        _logger.LogInformation("Successfully got all tags");
        return result;
    }
    
    [HttpGet(Name = "GetAllByType")]
    public ActionResult<IEnumerable<TagDto>> GetAllByType(string tagType)
    {
        var result = Ok(_service.GetAll(tagType));
        _logger.LogInformation("Successfully got all tags by type");
        return result;
    }
    
    [HttpGet(Name = "GetByName")]
    public ActionResult<TagDto> Get(string tagName)
    {
        var result =Ok(_service.Get(tagName));
        _logger.LogInformation("Successfully got all tags by name");
        return result;
    }

}