using scada_back.Exception;
using scada_back.Tag;
using scada_back.Tag.Model;
using scada_back.Tag.Model.Abstraction;
using scada_back.Validation;

namespace scada_back.Alarm;

public class AlarmService : IAlarmService
{
    private readonly IAlarmRepository _repository;
    private readonly ITagService _tagService;
    private readonly IValidationService _validationService;

    public AlarmService(IAlarmRepository repository,ITagService tagService, IValidationService validationService)
    {
        _repository = repository;
        _validationService = validationService;
        _tagService = tagService;
    }

    public IEnumerable<AlarmDto> GetAll()
    {
        IEnumerable<Alarm> alarms = _repository.GetAll().Result;
        return alarms.Select(alarm => alarm.ToDto());
    }
    
    public AlarmDto Get(string alarmName)
    {
        alarmName = alarmName.Trim();
        _validationService.ValidateEmptyString("alarmName", alarmName);

        Alarm alarm = _repository.Get(alarmName).Result;
        if (alarm == null)
        {
            throw new ObjectNotFoundException($"Alarm with name '{alarmName}' not found.");
        }
        return alarm.ToDto();
    }

    public AlarmDto Create(AlarmDto newAlarm)
    {
        _validationService.ValidateAlarm(newAlarm);
        
        Alarm existingAlarm = _repository.Get(newAlarm.AlarmName).Result;
        if (existingAlarm != null) {
            throw new ObjectNameTakenException($"Alarm with name '{newAlarm.AlarmName}' already exists.");
        }
        TagDto tag = _tagService.Get(newAlarm.TagName);
        if (tag is not AnalogInputTagDto analogTag)
        {
            throw new InvalidSignalTypeException("Alarm can be added only to analog tags.");
        }
        if (!(newAlarm.Limit < analogTag.HighLimit && newAlarm.Limit > analogTag.LowLimit))
        {
            throw new InvalidParameterException("Limit is not between high and low limit");
        }
        Alarm alarm = newAlarm.ToEntity();
        return _repository.Create(alarm).Result.ToDto();
    }

    public AlarmDto Delete(string alarmName)
    {
        alarmName = alarmName.Trim();
        _validationService.ValidateEmptyString("alarmName", alarmName);
        return _repository.Delete(alarmName).Result.ToDto();
    }

    public AlarmDto Update(AlarmDto updatedAlarm)
    {
        _validationService.ValidateAlarm(updatedAlarm);
        
        Alarm oldAlarm = _repository.Get(updatedAlarm.AlarmName).Result;
        if (oldAlarm == null)
        {
            throw new ObjectNotFoundException($"Alarm with name '{updatedAlarm.AlarmName}' not found.");
        }
        if (oldAlarm.TagName != updatedAlarm.TagName)
        {
            throw new System.Exception("Cannot change tag name");
        }
        return _repository.Update(updatedAlarm.ToEntity()).Result.ToDto();
    }
}