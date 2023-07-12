using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace scada_back.Infrastructure.Feature.AlarmHistory;

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
    
    [BsonElement("message")]
    public string Message { get; set; }

    public AlarmHistoryRecordDto ToDto()
    {
        return new AlarmHistoryRecordDto()
        {
            Timestamp = Timestamp,
            AlarmName = AlarmName,
            TagValue = TagValue,
            Message = Message
        };
    }
}

public class AlarmHistoryRecordDto
{
    public string AlarmName { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public double TagValue { get; set; }
    public string Message { get; set; }

    public AlarmHistoryRecord ToEntity()
    {
        return new AlarmHistoryRecord()
        {
            Timestamp = Timestamp,
            AlarmName = AlarmName,
            TagValue = TagValue,
            Message = Message
        };
    }

    public AlarmHistoryRecordWebSocketDto ToWebSocketDto(string tagName)
    {
        return new AlarmHistoryRecordWebSocketDto
        {
            AlarmName = AlarmName,
            TagName = tagName,
            Timestamp = Timestamp,
            TagValue = TagValue,
            Message = Message
        };
    }
}

public class AlarmHistoryRecordWebSocketDto
{
    public string AlarmName { get; set; } = string.Empty;
    
    public string TagName { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public double TagValue { get; set; }
    public string Message { get; set; }

}