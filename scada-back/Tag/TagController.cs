using Microsoft.AspNetCore.Mvc;
using scada_back.Tag.Model.Abstraction;

namespace scada_back.Tag;

[ApiController]
[Route("Api/[controller]/[action]")]
public class TagController : ControllerBase
{
    private readonly ITagService _tagService;
    private readonly ILogger<TagController> _logger;

    public TagController(ITagService tagService, ILogger<TagController> logger)
    {
        _tagService = tagService;
        _logger = logger;
    }

    [HttpGet(Name = "GetAllTags")]
    public ActionResult<IEnumerable<TagDto>> GetAll()
    {
        return Ok(_tagService.GetAll());
    }
    
    [HttpGet(Name = "GetAllTagsByType")]
    public ActionResult<IEnumerable<TagDto>> GetAllByType(string tagType)
    {
        return Ok(_tagService.GetAll(tagType));
    }
    
    [HttpGet(Name = "GetTagByName")]
    public ActionResult<TagDto> Get(string tagName)
    {
        return Ok(_tagService.Get(tagName));
    }
    
    [HttpPost(Name = "CreateTag")]
    public ActionResult<TagDto> Create([FromBody]TagDto tag)
    {
        return Ok(_tagService.Create(tag));
    }
    
    
    [HttpDelete(Name = "DeleteTag")]
    public ActionResult<TagDto> Delete(string tagName)
    {
        return Ok(_tagService.Delete(tagName));
    }
    
    [HttpPatch(Name = "UpdateTag")]
    public ActionResult<TagDto> Update([FromBody]TagDto tag)
    {
        return Ok(_tagService.Update(tag));
    }

}