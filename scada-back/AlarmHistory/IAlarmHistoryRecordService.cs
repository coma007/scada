namespace scada_back.AlarmHistory;

public interface IAlarmHistoryRecordService
{
    IEnumerable<AlarmHistoryRecordDTO> GetByName(string name);
    AlarmHistoryRecordDTO Create(AlarmHistoryRecordDTO createDto);
    IEnumerable<AlarmHistoryRecordDTO> GetAll();
    IEnumerable<AlarmHistoryRecordDTO> GetBetween(DateTime start, DateTime end);
    IEnumerable<AlarmHistoryRecordDTO> GetByPriority(int priority);
}