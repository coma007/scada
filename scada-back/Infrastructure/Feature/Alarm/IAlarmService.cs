namespace scada_back.Infrastructure.Feature.Alarm;

public interface IAlarmService
{
    public IEnumerable<AlarmDto> GetAll();
    public AlarmDto Get(string alarmName);
    public IEnumerable<AlarmDto> GetInvoked(string tagName, double value);
    public AlarmDto Create(AlarmDto newAlarm);
    public AlarmDto Delete(string alarmName);
    public AlarmDto Update(AlarmDto updatedAlarm);
}