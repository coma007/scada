namespace scada_back.Infrastructure.Feature.Alarm;

public interface IAlarmRepository
{
    public Task<IEnumerable<Alarm>> GetAll();
    public Task<Alarm> Get(string alarmName);
    Task<IEnumerable<Alarm>> GetInvoked(string tagName, double value);
    public Task<Alarm> Create(Alarm newAlarm);
    public Task<Alarm> Delete(string alarmName);
    public Task<Alarm> Update(Alarm updatedAlarm);
    public Task<IEnumerable<Alarm>> getByTagName(string name);
}