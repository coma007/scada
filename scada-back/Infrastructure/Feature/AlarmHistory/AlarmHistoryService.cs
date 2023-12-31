using scada_back.Api.WebSocket;
using scada_back.Infrastructure.Feature.Alarm;
using scada_back.Infrastructure.Feature.Alarm.Enumeration;

namespace scada_back.Infrastructure.Feature.AlarmHistory;

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
        newRecord.Timestamp = DateTime.Now;
        _repository.Create(record);
        _logger.LogToFile(record);
    }

    public IEnumerable<AlarmHistoryRecordDto> AlarmIfNeeded(string tagName, double value)
    {
        IEnumerable<AlarmDto> alarms = _alarmService.GetInvoked(tagName, value)
            .OrderByDescending(a => a.AlarmPriority);
        List<AlarmHistoryRecordDto> alarmRecords = new List<AlarmHistoryRecordDto>();

        foreach (var alarm in alarms)
        {
            AlarmHistoryRecordDto newRecord = new AlarmHistoryRecordDto
            {
                AlarmName = alarm.AlarmName, 
                Timestamp = DateTime.Now, 
                TagValue = value,
                Message = EvaluateMessage(tagName,  value, alarm)
            };
            Create(newRecord);
            alarmRecords.Add(newRecord);
        }

        return alarmRecords;
    }

    private string EvaluateMessage(string tagName,  double tagValue, AlarmDto alarm)
    {
        return $"Value of {tagName} ({tagValue}) is critically {alarm.Type.ToLower()} limit ({alarm.Limit})";
    }
}