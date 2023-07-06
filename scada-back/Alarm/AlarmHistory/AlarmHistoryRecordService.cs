using System.Collections;
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

    public IEnumerable<AlarmHistoryRecordDTO> GetByName(string name)
    {
        IEnumerable<AlarmHistoryRecord> result = _repository.GetByName(name).Result; 
        return result.Count() > 0 ? result.Select(record => record.ToDto()) : Enumerable.Empty<AlarmHistoryRecordDTO>();
    }

    public AlarmHistoryRecordDTO Create(AlarmHistoryRecordDTO createDto)
    {
        if (_alarmRepository.Get(createDto.AlarmName).Result == null)
            throw new System.Exception("There is no alarm with such name");
        AlarmHistoryRecord newRecord = createDto.ToEntity();
        return _repository.Create(newRecord).Result.ToDto();
    }

    public IEnumerable<AlarmHistoryRecordDTO> GetAll()
    {
        IEnumerable<AlarmHistoryRecord> records = _repository.GetAll().Result;
        return records.Count() > 0 ? records.Select(alarm => alarm.ToDto()) : Enumerable.Empty<AlarmHistoryRecordDTO>();
    }

    public IEnumerable<AlarmHistoryRecordDTO> GetBetween(DateTime start, DateTime end)
    {
        IEnumerable<AlarmHistoryRecord> records = _repository.GetBetween(start, end).Result;
        return records.Count() > 0 ? records.Select(alarm => alarm.ToDto()) : Enumerable.Empty<AlarmHistoryRecordDTO>();
    }
}