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

    [HttpGet(Name = "GetAllTags")]
    public ActionResult<IEnumerable<TagDto>> GetAll()
    {
        return Ok(_service.GetAll());
    }
    
    [HttpGet(Name = "GetAllByType")]
    public ActionResult<IEnumerable<TagDto>> GetAllByType(string tagType)
    {
        return Ok(_service.GetAll(tagType));
    }
    
    [HttpGet(Name = "GetByName")]
    public ActionResult<TagDto> Get(string tagName)
    {
        return Ok(_service.Get(tagName));
    }

}