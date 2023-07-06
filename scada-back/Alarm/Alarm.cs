using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace scada_back.Alarm;

public class Alarm
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
    [BsonElement("type")]
    public AlarmType Type { get; set; }
    [BsonElement("priority")]
    public Priority Priority { get; set; }
    [BsonElement("limit")]
    public double Limit { get; set; }
    [BsonElement("alarm_name")]
    public string AlarmName { get; set; } = string.Empty;
    [BsonElement("tag_id")]
    public string TagId { get; set; } = string.Empty;

    public Alarm(AlarmType type, Priority priority, double limit, string alarmName, string tagId)
    {
        Type = type;
        Priority = priority;
        Limit = limit;
        AlarmName = alarmName;
        TagId = tagId;
    }

    public Alarm()
    {
    }

    public AlarmDTO ToDto()
    {
        return new AlarmDTO()
        {
            Type = Type,
            AlarmName = AlarmName,
            Limit = Limit,
            TagId = TagId,
            Priority = Priority
        };
    }
}