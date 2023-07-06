using System.Collections;
using scada_back.Exception;

namespace scada_back.Alarm;

public class AlarmService : IAlarmService
{
    private readonly IAlarmRepository _repository;

    public AlarmService(IAlarmRepository repository)
    {
        _repository = repository;
    }
    public AlarmDTO GetByName(string name)
    {
        Alarm result = _repository.GetByName(name).Result; 
        return result != null ? result.ToDto() : throw new ObjectNotFound("Not found");
    }

    public IEnumerable<AlarmDTO> GetAll()
    {
        IEnumerable<Alarm> alarms = _repository.GetAll().Result;
        return alarms.Count() > 0 ? alarms.Select(alarm => alarm.ToDto()) : Enumerable.Empty<AlarmDTO>();
    }

    public AlarmDTO Create(AlarmDTO updateDto)
    {
        if (_repository.GetByName(updateDto.AlarmName).Result != null)
            throw new System.Exception("Already exists alarm with that name");
        Alarm newAlarm = updateDto.ToEntity();
        return _repository.Create(newAlarm).Result.ToDto();
    }

    public AlarmDTO Delete(string id)
    {
        return _repository.Delete(id).Result.ToDto();
    }
    
}