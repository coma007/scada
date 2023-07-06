
namespace scada_back.Alarm.AlarmHistory;

public interface IAlarmHistoryRecordRepository
{
    Task<AlarmHistoryRecord> Get(string id);
    Task<AlarmHistoryRecord> Create(AlarmHistoryRecord newRecord);
    Task<IEnumerable<AlarmHistoryRecord>> GetAll();
    Task<IEnumerable<AlarmHistoryRecord>> GetBetween(DateTime start, DateTime end);
}