using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using scada_back.Alarm.Enumeration;

namespace scada_back.Alarm;

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
        return new AlarmDto
        {
            Type = Type,
            AlarmName = AlarmName,
            Limit = Limit,
            TagName = TagName,
            AlarmPriority = AlarmPriority
        };
    }
}


public class AlarmDto
{
    public AlarmType Type { get; set; }
    public AlarmPriority AlarmPriority { get; set; }
    public double Limit { get; set; }
    public string AlarmName { get; set; } = string.Empty;
    public string TagName { get; set; } = string.Empty;

    public Alarm ToEntity()
    {
        return new Alarm()
        {
            AlarmName = AlarmName,
            Type = Type,
            TagName = TagName,
            Limit = Limit,
            AlarmPriority = AlarmPriority
        };
    }
}