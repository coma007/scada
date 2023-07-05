namespace scada_back.Alarm;

public interface IAlarmRepository
{
    public Task<IEnumerable<Alarm>> GetAll();
    public Task<Alarm> Get(string alarmName);
    public Task<Alarm> Create(Alarm newAlarm);
    public Task<Alarm> Delete(string alarmName);
    public Task<Alarm> Update(Alarm updatedAlarm);
}