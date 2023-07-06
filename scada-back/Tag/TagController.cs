using Microsoft.AspNetCore.Mvc;
using scada_back.Tag.Model.Abstraction;

namespace scada_back.Tag;

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
    
    [HttpGet(Name = "GetAllTagsByType")]
    public ActionResult<IEnumerable<TagDto>> GetAllByType(string tagType)
    {
        return Ok(_service.GetAll(tagType));
    }
    
    [HttpGet(Name = "GetTagByName")]
    public ActionResult<TagDto> Get(string tagName)
    {
        return Ok(_service.Get(tagName));
    }
    
    [HttpPost(Name = "CreateTag")]
    public ActionResult<TagDto> Create([FromBody]TagDto tag)
    {
        return Ok(_service.Create(tag));
    }
    
    
    [HttpDelete(Name = "DeleteTag")]
    public ActionResult<TagDto> Delete(string tagName)
    {
        return Ok(_service.Delete(tagName));
    }
    
    [HttpPatch(Name = "UpdateTag")]
    public ActionResult<TagDto> Update([FromBody]TagDto tag)
    {
        return Ok(_service.Update(tag));
    }
    
    [HttpPatch(Name = "UpdateTagScan")]
    public ActionResult<TagDto> UpdateScan(string tagName)
    {
        return Ok(_service.UpdateScan(tagName));
    }

}