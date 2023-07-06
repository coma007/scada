namespace scada_back.Alarm.AlarmHistory;

public class AlarmHistoryRecordCreateDTO
{
    public string AlarmName { get; set; }
    public DateTime Timestamp { get; set; }
    public double TagValue { get; set; }

    public AlarmHistoryRecordDTO ToRegularDTO()
    {
        return new AlarmHistoryRecordDTO()
        {
            Timestamp = Timestamp,
            AlarmName = AlarmName,
            TagValue = TagValue
        };
    }
}