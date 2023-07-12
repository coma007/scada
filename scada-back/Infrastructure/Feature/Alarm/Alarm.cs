using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using scada_back.Infrastructure.Feature.Alarm.Enumeration;

namespace scada_back.Infrastructure.Feature.Alarm;

public class Alarm
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
    [BsonElement("type")]
    public AlarmType Type { get; set; }
    [BsonElement("priority")]
    public AlarmPriority AlarmPriority { get; set; }
    [BsonElement("limit")]
    public double Limit { get; set; }
    [BsonElement("alarm_name")]
    public string AlarmName { get; set; } = string.Empty;
    [BsonElement("tag_id")]
    public string TagName { get; set; } = string.Empty;

    public AlarmDto ToDto()
    {
        string alarmType = "BELLOW";
        if (Type == AlarmType.Above)
        {
            alarmType = "ABOVE";
        }
        return new AlarmDto
        {
            Type = alarmType,
            AlarmName = AlarmName,
            Limit = Limit,
            TagName = TagName,
            AlarmPriority = AlarmPriority
        };
    }
}


public class AlarmDto
{
    public string Type { get; set; } = string.Empty;
    public AlarmPriority AlarmPriority { get; set; }
    public double Limit { get; set; }
    public string AlarmName { get; set; } = string.Empty;
    public string TagName { get; set; } = string.Empty;

    public Alarm ToEntity()
    {
        AlarmType alarmType = AlarmType.Bellow;
        if (Type.ToUpper() == "ABOVE")
        {
            alarmType = AlarmType.Above;
        }
        return new Alarm()
        {
            AlarmName = AlarmName,
            Type = alarmType,
            TagName = TagName,
            Limit = Limit,
            AlarmPriority = AlarmPriority
        };
    }
}