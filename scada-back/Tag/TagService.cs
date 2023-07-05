using scada_back.Exception;
using scada_back.Tag.Model.Abstraction;

namespace scada_back.Tag;

public class TagService : ITagService
{
    private readonly ITagRepository _repository;

    public TagService(ITagRepository repository)
    {
        _repository = repository;
    }
    
    public IEnumerable<TagDto> GetAll()
    {
        IEnumerable<Model.Abstraction.Tag> tags =  _repository.GetAll().Result;
        return tags.Select(tag => tag.ToDto());
    }

    public IEnumerable<TagDto> GetAll(string discriminator)
    {
        IEnumerable<Model.Abstraction.Tag> tags = _repository.GetAll(discriminator).Result;
        return tags.Select(tag => tag.ToDto());
    }
    
    public TagDto Get(string tagName)
    {
        Model.Abstraction.Tag tag =  _repository.Get(tagName).Result;
        if (tag == null)
        {
            throw new ObjectNotFoundException($"Tag with name '{tagName}' not found.");
        }
        return tag.ToDto();
    }

    public TagDto Create(TagDto newTag)
    {
        Model.Abstraction.Tag existingTag = _repository.Get(newTag.TagName).Result;
        if (existingTag != null)
        {
            throw new ObjectNameTakenException($"Tag with name '{newTag.TagName}' already exists.");
        }
        Model.Abstraction.Tag tag = newTag.ToEntity();
        return _repository.Create(tag).Result.ToDto();
    }

    public TagDto Delete(string tagName)
    {
        return _repository.Delete(tagName).Result.ToDto();
    }

    public TagDto Update(TagDto updatedTag)
    {
        return _repository.Update(updatedTag.ToEntity()).Result.ToDto();
    }
}