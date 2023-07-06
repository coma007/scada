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
            throw new ObjectNotFoundException($"Alarm with name {createDto.AlarmName} is not found");
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
        IEnumerable<AlarmHistoryRecord> records = SortByPriorityThenTime(_repository.GetBetween(start, end).Result);
        return records.Count() > 0 ? records.Select(alarm => alarm.ToDto()) : Enumerable.Empty<AlarmHistoryRecordDTO>();
    }

    private IEnumerable<AlarmHistoryRecord> SortByPriorityThenTime(IEnumerable<AlarmHistoryRecord> records)
    {
        return records
            .OrderByDescending(x =>
            {
                Alarm alarm = _alarmRepository.Get(x.AlarmName).Result;
                if(alarm == null)
                    throw new ObjectNotFoundException($"Alarm with name {alarm.AlarmName} is not found");
                return alarm.AlarmPriority;
            })
            .ThenByDescending(x => x.Timestamp);
    }
}