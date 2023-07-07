using scada_back.Alarm;

namespace scada_back.AlarmHistory;

public class AlarmHistoryService : IAlarmHistoryService
{
    private readonly IAlarmHistoryRepository _repository;
    private readonly IAlarmService _alarmService;
    private readonly IAlarmHistoryLogger _logger;

    public AlarmHistoryService(IAlarmHistoryRepository repository, IAlarmService alarmService, IAlarmHistoryLogger logger)
    {
        _repository = repository;
        _alarmService = alarmService;
        _logger = logger;
    }

    public IEnumerable<AlarmHistoryRecordDto> GetAll()
    {
        IEnumerable<AlarmHistoryRecord> records = _repository.GetAll().Result;
        return records.Select(alarm => alarm.ToDto());
    }
    
    public IEnumerable<AlarmHistoryRecordDto> Get(string alarmName)
    {
        IEnumerable<AlarmHistoryRecord> result = _repository.GetByName(alarmName).Result; 
        return result.Select(record => record.ToDto());
    }

    public IEnumerable<AlarmHistoryRecordDto> Get(int priority)
    {
        IEnumerable<AlarmHistoryRecord> records = _repository.GetByPriority(priority).Result;
        return records.Select(alarm => alarm.ToDto());
    }
    
    public IEnumerable<AlarmHistoryRecordDto> GetBetween(DateTime start, DateTime end)
    {
        IEnumerable<AlarmHistoryRecord> records = _repository.GetBetween(start, end).Result;
        return records.Select(alarm => alarm.ToDto());
    }
    
    public void Create(AlarmHistoryRecordDto newRecord)
    {
        _alarmService.Get(newRecord.AlarmName);
        
        AlarmHistoryRecord record = newRecord.ToEntity();
        _repository.Create(record);
        _logger.LogToFile(record);
    }

}