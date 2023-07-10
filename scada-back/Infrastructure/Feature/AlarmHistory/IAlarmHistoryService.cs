namespace scada_back.Infrastructure.Feature.AlarmHistory;

public interface IAlarmHistoryService
{
    IEnumerable<AlarmHistoryRecordDto> GetAll();
    IEnumerable<AlarmHistoryRecordDto> Get(string alarmName);
    IEnumerable<AlarmHistoryRecordDto> Get(int priority);
    IEnumerable<AlarmHistoryRecordDto> GetBetween(DateTime start, DateTime end);
    void Create(AlarmHistoryRecordDto newRecord);
    IEnumerable<AlarmHistoryRecordDto> AlarmIfNeeded(string tagName, double value);
}