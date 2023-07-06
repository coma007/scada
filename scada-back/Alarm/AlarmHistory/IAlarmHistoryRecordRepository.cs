
using System.Collections;

namespace scada_back.Alarm.AlarmHistory;

public interface IAlarmHistoryRecordRepository
{
    Task<IEnumerable<AlarmHistoryRecord>> GetByName(string name);
    Task<AlarmHistoryRecord> Create(AlarmHistoryRecord newRecord);
    Task<IEnumerable<AlarmHistoryRecord>> GetAll();
    Task<IEnumerable<AlarmHistoryRecord>> GetBetween(DateTime start, DateTime end);
    Task<IEnumerable<AlarmHistoryRecord>> GetByPriority(int priority);
}