
namespace scada_back.Alarm.AlarmHistory;

public interface IAlarmHistoryRecordService
{
    AlarmHistoryRecordDTO GetByName(string name);
    AlarmHistoryRecordDTO Create(AlarmHistoryRecordDTO createDto);
    IEnumerable<AlarmHistoryRecordDTO> GetAll();
    IEnumerable<AlarmHistoryRecordDTO> GetBetween(DateTime start, DateTime end);
}