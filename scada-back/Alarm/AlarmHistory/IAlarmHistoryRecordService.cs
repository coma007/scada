
using System.Collections;

namespace scada_back.Alarm.AlarmHistory;

public interface IAlarmHistoryRecordService
{
    IEnumerable<AlarmHistoryRecordDTO> GetByName(string name);
    AlarmHistoryRecordDTO Create(AlarmHistoryRecordDTO createDto);
    IEnumerable<AlarmHistoryRecordDTO> GetAll();
    IEnumerable<AlarmHistoryRecordDTO> GetBetween(DateTime start, DateTime end);
    IEnumerable<AlarmHistoryRecordDTO> GetByPriority(int priority);
}