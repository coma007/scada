namespace scada_back.AlarmHistory;

public interface IAlarmHistoryService
{
    IEnumerable<AlarmHistoryRecordDto> GetAll();
    IEnumerable<AlarmHistoryRecordDto> Get(string alarmName);
    IEnumerable<AlarmHistoryRecordDto> Get(int priority);
    IEnumerable<AlarmHistoryRecordDto> GetBetween(DateTime start, DateTime end);
    void Create(AlarmHistoryRecordDto newRecord);
}