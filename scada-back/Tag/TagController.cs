using Microsoft.AspNetCore.Mvc;
using scada_back.Exception;
using scada_back.Tag.Model;
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

    [HttpGet(Name = "GetAll")]
    public ActionResult<IEnumerable<TagDTO>> GetAll()
    {
        IEnumerable<TagDTO> tags;
        try
        {
            tags = _tagService.GetAll();
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok(tags);
    }
    
    [HttpGet(Name = "GetAllByType")]
    public ActionResult<IEnumerable<TagDTO>> GetAllByType(string tagType)
    {
        IEnumerable<TagDTO> tags;
        try
        {
            tags = _tagService.GetAll(tagType);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok(tags);
    }
    
    [HttpGet(Name = "GetByName")]
    public ActionResult<TagDTO> Get(string tagName)
    {
        TagDTO tag;
        try
        {
            tag = _tagService.Get(tagName);
        }
        catch (ObjectNotFound e)
        {
            return NotFound(e.Message);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok(tag);
    }
    
    [HttpPost(Name = "Create")]
    public ActionResult<TagDTO> Create(TagDTO tag)
    {
        TagDTO newTag;
        try
        {
            newTag = _tagService.Create(tag);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok(newTag);
    }
    
}