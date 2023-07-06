using scada_back.Exception;

namespace scada_back.Alarm.AlarmHistory;

public class AlarmHistoryRecordService : IAlarmHistoryRecordService
{
    private readonly IAlarmHistoryRecordRepository _repository;
    private readonly IAlarmRepository _alarmRepository;

    public AlarmHistoryRecordService(IAlarmHistoryRecordRepository repository, IAlarmRepository alarmRepository)
    {
        _repository = repository;
        _alarmRepository = alarmRepository;
    }

    public AlarmHistoryRecordDTO Get(string id)
    {
        AlarmHistoryRecord result = _repository.Get(id).Result; 
        return result != null ? result.ToDto() : throw new ObjectNotFound("Not found");
    }

    public AlarmHistoryRecordDTO Create(AlarmHistoryRecordCreateDTO createDto)
    {
        if (_alarmRepository.GetByName(createDto.AlarmName).Result == null)
            throw new System.Exception("There is no alarm with such name");
        AlarmHistoryRecord newRecord = createDto.ToRegularDTO().ToEntity();
        return _repository.Create(newRecord).Result.ToDto();
    }

    public IEnumerable<AlarmHistoryRecordDTO> GetAll()
    {
        IEnumerable<AlarmHistoryRecord> records = _repository.GetAll().Result;
        return records.Count() > 0 ? records.Select(alarm => alarm.ToDto()) : Enumerable.Empty<AlarmHistoryRecordDTO>();
    }
}