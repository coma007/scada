namespace scada_back.Infrastructure.Feature.AlarmHistory;

public interface IAlarmHistoryRepository
{
    Task<IEnumerable<AlarmHistoryRecord>> GetAll();
    Task<IEnumerable<AlarmHistoryRecord>> GetByName(string name);
    Task<IEnumerable<AlarmHistoryRecord>> GetByPriority(int priority);
    Task<IEnumerable<AlarmHistoryRecord>> GetBetween(DateTime start, DateTime end);
    void Create(AlarmHistoryRecord newRecord);
}