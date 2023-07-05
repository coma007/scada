namespace scada_back.Alarm;

public interface IAlarmService
{
    public IEnumerable<AlarmDto> GetAll();
    public AlarmDto Get(string alarmName);
    public AlarmDto Create(AlarmDto newAlarm);
    public AlarmDto Delete(string alarmName);
    public AlarmDto Update(AlarmDto updatedAlarm);
}