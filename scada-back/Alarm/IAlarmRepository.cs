namespace scada_back.Alarm;

public interface IAlarmRepository
{
    public Task<IEnumerable<Alarm>> GetAll();
    public Task<Alarm> Create(Alarm newAlarm);
    public Task<Alarm> Delete(string id);
    public Task<Alarm> GetByName(string name);
}