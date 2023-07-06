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
    public AlarmDTO Get(string id)
    {
        Alarm result = _repository.Get(id).Result; 
        return result != null ? result.ToDto() : throw new ObjectNotFound("Not found");
    }

    public IEnumerable<AlarmDTO> GetAll()
    {
        IEnumerable<Alarm> alarms = _repository.GetAll().Result;
        return alarms.Count() > 0 ? alarms.Select(alarm => alarm.ToDto()) : Enumerable.Empty<AlarmDTO>();
    }

    public AlarmDTO Create(AlarmCreateUpdateDTO updateDto)
    {
        if (_repository.GetByName(updateDto.AlarmName).Result != null)
            throw new System.Exception("Already exists alarm with that name");
        Alarm newAlarm = updateDto.ToRegularDTO().ToEntity();
        return _repository.Create(newAlarm).Result.ToDto();
    }

    public AlarmDTO Delete(string id)
    {
        return _repository.Delete(id).Result.ToDto();
    }

    public AlarmDTO Update(AlarmCreateUpdateDTO dto, string id)
    {
        Alarm sameName = _repository.GetByName(dto.AlarmName).Result;
        if (sameName != null)
        {
            if ( sameName.Id != id)
                throw new System.Exception("Already exists alarm with that name");
        }

        AlarmDTO updatedDto = dto.ToRegularDTO();
        updatedDto.Id = id;
        return _repository.Update(updatedDto.ToEntity()).Result.ToDto();
    }
}