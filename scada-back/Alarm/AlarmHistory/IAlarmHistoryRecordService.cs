namespace scada_back.Alarm.AlarmHistory;

public interface IAlarmHistoryRecordService
{
    AlarmHistoryRecordDTO Get(string id);
    AlarmHistoryRecordDTO Create(AlarmHistoryRecordCreateDTO createDto);
    IEnumerable<AlarmHistoryRecordDTO> GetAll();
}