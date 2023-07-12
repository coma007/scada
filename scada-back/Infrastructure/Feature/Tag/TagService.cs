using Microsoft.VisualBasic.CompilerServices;
using scada_back.Infrastructure.Exception;
using scada_back.Infrastructure.Feature.DriverState;
using scada_back.Infrastructure.Feature.Tag.Model.Abstraction;
using scada_back.Infrastructure.Feature.TagHistory;
using scada_back.Infrastructure.Validation;

namespace scada_back.Infrastructure.Feature.Tag;

public class TagService : ITagService
{
    private readonly ITagRepository _repository;
    private readonly IDriverStateService _driverStateService;
    private readonly IValidationService _validationService;
    private readonly IConfiguration _configuration;

    public TagService(ITagRepository repository, IDriverStateService driverStateService, IValidationService validationService, IConfiguration configuration)
    {
        _repository = repository;
        _driverStateService = driverStateService;
        _validationService = validationService;
        _configuration = configuration;
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

    public IEnumerable<string> GetAllNames(string signalType)
    {
        signalType = signalType.ToLower().Trim();
        _validationService.ValidateEmptyString("signalType",signalType);
        _validationService.ValidateSignalType(signalType);
        
        return _repository.GetAllNames(signalType).Result;
    }

    public IEnumerable<string> GetInputScanNames()
    {
        return _repository.GetInputScanNames().Result;
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

        if (newTag is IInputTagDto inputTagDto)
        {
             int simulationLimit = Int32.Parse(_configuration.GetSection("Driver:SimulationAddressLimit").Value);
             if (inputTagDto.Driver.ToUpper() == "SIMULATION" && newTag.IOAddress >= simulationLimit)
             {
                 throw new InvalidParameterException(
                     $"You are trying to write simulation tag to address that is for reserved for realtime, try value less than {simulationLimit}");
             }
             if (inputTagDto.Driver.ToUpper() == "REALTIME" && newTag.IOAddress < simulationLimit)
             {
                 throw new InvalidParameterException(
                     $"You are trying to write real time tag to address that is for reserved for simulation, try value bigger or equal than {simulationLimit}");
             }

             if (inputTagDto.Driver.ToUpper() == "SIMULATION")
             {
                 int analogLimit = Int32.Parse(_configuration.GetSection("Driver:SimulationAddressAnalogLimit").Value);
                 if (inputTagDto is IAnalogTagDto && newTag.IOAddress >= analogLimit)
                 {
                     throw new InvalidParameterException(
                         $"You are trying to write analog value in digital are, try value less than {analogLimit}");
                 }
                 if (inputTagDto is IDigitalTagDto && newTag.IOAddress < analogLimit)
                 {
                     throw new InvalidParameterException(
                         $"You are trying to write digital value in analog are, try value greater or equal than {analogLimit}");
                 }
             }
        }

        Model.Abstraction.Tag tag = newTag.ToEntity();
        return _repository.Create(tag).Result.ToDto();
    }

    public TagDto Delete(string tagName)
    {
        tagName = tagName.Trim();
        _validationService.ValidateEmptyString("tagName", tagName);
        try
        {
            return _repository.Delete(tagName).Result.ToDto();
        }
        catch (AggregateException e)
        {
            throw e.InnerException!;
        }
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
    
    public TagDto UpdateOutputValue(string tagName, double value)
    {
        tagName = tagName.Trim();
        _validationService.ValidateEmptyString("tagName", tagName);

        Model.Abstraction.Tag tag = _repository.Get(tagName).Result;
        if (tag is IInputTag)
        {
            throw new InvalidSignalTypeException($"Can only change value of output tag.");
        }

        if (tag is IDigitalTag)
        {
            _validationService.ValidateDigitalValue(value);
        }

        if (tag is IAnalogTag analogTag)
        {
            if (value < analogTag.LowLimit || value > analogTag.HighLimit)
            {
                throw new InvalidSignalTypeException($"Value is not between limits {analogTag.LowLimit} and {analogTag.HighLimit}");
            }
            
        }

        ((IOutputTag)tag).InitialValue = value;
        return _repository.Update(tag).Result.ToDto();
    }
}