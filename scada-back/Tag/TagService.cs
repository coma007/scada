using scada_back.DriverState;
using scada_back.Exception;
using scada_back.Tag.Model.Abstraction;
using scada_back.Validation;

namespace scada_back.Tag;

public class TagService : ITagService
{
    private readonly ITagRepository _repository;
    private readonly IDriverStateService _driverStateService;
    private readonly IValidationService _validationService;

    public TagService(ITagRepository repository, IDriverStateService driverStateService, IValidationService validationService)
    {
        _repository = repository;
        _driverStateService = driverStateService;
        _validationService = validationService;
    }
    
    public IEnumerable<TagDto> GetAll()
    {
        IEnumerable<Model.Abstraction.Tag> tags =  _repository.GetAll().Result;
        return tags.Select(tag => tag.ToDto());
    }

    public IEnumerable<TagDto> GetAll(string discriminator)
    {
        discriminator = discriminator.ToLower().Trim();
        _validationService.ValidateEmptyString("tagType", discriminator);
        
        IEnumerable<Model.Abstraction.Tag> tags = _repository.GetAll(discriminator).Result;
        return tags.Select(tag => tag.ToDto());
    }

    public Task<IEnumerable<string>> GetAllNames(string signalType)
    {
        signalType = signalType.ToLower().Trim();
        _validationService.ValidateEmptyString("signalType",signalType);
        _validationService.ValidateSignalType(signalType);
        
        return _repository.GetAllNames(signalType);
    }

    public TagDto Get(string tagName)
    {
        tagName = tagName.Trim();
        _validationService.ValidateEmptyString("tagName", tagName);
        
        Model.Abstraction.Tag tag =  _repository.Get(tagName).Result;
        if (tag == null)
        {
            throw new ObjectNotFoundException($"Tag with name '{tagName}' not found.");
        }
        return tag.ToDto();
    }

    public TagDto Create(TagDto newTag)
    {
        _validationService.ValidateTag(newTag);
        _driverStateService.Get(newTag.IOAddress);
        
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
        tagName = tagName.Trim();
        _validationService.ValidateEmptyString("tagName", tagName);

        return _repository.Delete(tagName).Result.ToDto();
    }

    public TagDto Update(TagDto updatedTag)
    {
        _validationService.ValidateTag(updatedTag);
        
        return _repository.Update(updatedTag.ToEntity()).Result.ToDto();
    }

    public TagDto UpdateScan(string tagName)
    {
        tagName = tagName.Trim();
        _validationService.ValidateEmptyString("tagName", tagName);

        Model.Abstraction.Tag tag = _repository.Get(tagName).Result;
        if (tag is IInputTag inputTag)
        {
            inputTag.Scan = !inputTag.Scan;
        }
        else
        {
            throw new InvalidSignalTypeException($"Tag with {tagName} is not input tag.");
        }
        return _repository.Update(tag).Result.ToDto();

    }
}