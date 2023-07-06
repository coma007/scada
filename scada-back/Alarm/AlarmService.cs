using System.Collections;
using scada_back.Exception;
using scada_back.Validation;

namespace scada_back.Alarm;

public class AlarmService : IAlarmService
{
    private readonly IAlarmRepository _repository;
    private readonly IValidationService _validationService;

    public AlarmService(IAlarmRepository repository, IValidationService validationService)
    {
        _repository = repository;
        _validationService = validationService;
    }

    public IEnumerable<AlarmDto> GetAll()
    {
        IEnumerable<Alarm> alarms = _repository.GetAll().Result;
        return alarms.Select(alarm => alarm.ToDto());
    }
    
    public AlarmDto Get(string alarmName)
    {
        Alarm alarm = _repository.Get(alarmName).Result;
        if (alarm == null)
        {
            throw new ObjectNotFoundException($"Alarm with '{alarmName}' not found.");
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
        Alarm alarm = newAlarm.ToEntity();
        return _repository.Create(alarm).Result.ToDto();
    }

    public AlarmDto Delete(string alarmName)
    {
        return _repository.Delete(alarmName).Result.ToDto();
    }

    public AlarmDto Update(AlarmDto updatedAlarm)
    {
        _validationService.ValidateAlarm(updatedAlarm);
        return _repository.Update(updatedAlarm.ToEntity()).Result.ToDto();
    }
}