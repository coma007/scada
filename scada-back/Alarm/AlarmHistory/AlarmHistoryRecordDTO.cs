namespace scada_back.Alarm.AlarmHistory;

public class AlarmHistoryRecordDTO
{
    public string Id { get; set; }
    public string AlarmName { get; set; }
    public DateTime Timestamp { get; set; }
    public double TagValue { get; set; }

    public AlarmHistoryRecord ToEntity()
    {
        return new AlarmHistoryRecord()
        {
            Id = Id,
            Timestamp = Timestamp,
            AlarmName = AlarmName,
            TagValue = TagValue
        };
    }
}