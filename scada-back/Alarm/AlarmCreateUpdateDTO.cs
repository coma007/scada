namespace scada_back.Alarm;

public class AlarmCreateUpdateDTO
{
    public AlarmType Type { get; set; }
    public Priority Priority { get; set; }
    public double Limit { get; set; }
    public string AlarmName { get; set; } = string.Empty;
    public string TagId { get; set; } = string.Empty;

    public AlarmCreateUpdateDTO(AlarmType type, Priority priority, double limit, string alarmName, string tagId)
    {
        Type = type;
        Priority = priority;
        Limit = limit;
        AlarmName = alarmName;
        TagId = tagId;
    }

    public AlarmCreateUpdateDTO()
    {
    }

    public AlarmDTO ToRegularDTO()
    {
        return new AlarmDTO()
        {
            AlarmName = AlarmName,
            Type = Type,
            TagId = TagId,
            Limit = Limit,
            Priority = Priority
        };
    }
}