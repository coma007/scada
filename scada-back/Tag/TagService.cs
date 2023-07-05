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
    
    public IEnumerable<TagDTO> GetAll()
    {
        IEnumerable<Model.Abstraction.Tag> tags =  _repository.GetAll().Result;
        return tags.Select(tags => tags.ToDTO());
    }

    public IEnumerable<TagDTO> GetAll(string discriminator)
    {
        IEnumerable<Model.Abstraction.Tag> tags = _repository.GetAll(discriminator).Result;
        return tags.Select(tags => tags.ToDTO());
    }
    
    public TagDTO Get(string tagName)
    {
        Model.Abstraction.Tag tag =  _repository.Get(tagName).Result;
        if (tag == null)
        {
            throw new ObjectNotFound($"Tag with '{tagName}' not found.");
        }
        return tag.ToDTO();
    }

    public TagDTO Create(TagDTO tag)
    {
        Model.Abstraction.Tag existingTag = _repository.Get(tag.TagName).Result;
        if (existingTag != null)
        {
            throw new ObjectNameAlreadyExists($"Tag with name '{tag.TagName}' already exists.");
        }
        Model.Abstraction.Tag newTag = tag.ToEntity();
        return _repository.Create(newTag).Result.ToDTO();
    }

    public TagDTO Delete(string tagName)
    {
        throw new NotImplementedException();
    }
}