using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace scada_back.AlarmHistory;

public class AlarmHistoryRecord
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("alarm_name")] 
    public string AlarmName { get; set; } = string.Empty;
    [BsonElement("timestamp")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime Timestamp { get; set; }
    [BsonElement("tag_value")]
    public double TagValue { get; set; }

    public AlarmHistoryRecordDto ToDto()
    {
        return new AlarmHistoryRecordDto()
        {
            Timestamp = Timestamp,
            AlarmName = AlarmName,
            TagValue = TagValue
        };
    }
}

public class AlarmHistoryRecordDto
{
    public string AlarmName { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public double TagValue { get; set; }

    public AlarmHistoryRecord ToEntity()
    {
        return new AlarmHistoryRecord()
        {
            Timestamp = Timestamp,
            AlarmName = AlarmName,
            TagValue = TagValue
        };
    }
}