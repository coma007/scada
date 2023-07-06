using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace scada_back.AlarmHistory;

public class AlarmHistoryRecord
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
    [BsonElement("alarm_name")]
    public string AlarmName { get; set; }
    [BsonElement("timestamp")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime Timestamp { get; set; }
    [BsonElement("tag_value")]
    public double TagValue { get; set; }

    public AlarmHistoryRecordDTO ToDto()
    {
        return new AlarmHistoryRecordDTO()
        {
            Timestamp = Timestamp,
            AlarmName = AlarmName,
            TagValue = TagValue
        };
    }
}

public class AlarmHistoryRecordDTO
{
    public string AlarmName { get; set; }
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