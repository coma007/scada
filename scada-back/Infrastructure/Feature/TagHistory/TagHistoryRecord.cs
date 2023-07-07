using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace scada_back.Infrastructure.Feature.TagHistory;

public class TagHistoryRecord
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
    [BsonElement("tag_name")]
    public string TagName { get; set; } = string.Empty;
    [BsonElement("timestamp")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime Timestamp { get; set; }
    [BsonElement("tag_value")]
    public double TagValue { get; set; }

    public TagHistoryRecordDto ToDto()
    {
        return new TagHistoryRecordDto
        {
            TagName = this.TagName,
            Timestamp = this.Timestamp,
            TagValue = this.TagValue
        };
    }
}


public class TagHistoryRecordDto
{
    public string TagName { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public double TagValue { get; set; }

    public TagHistoryRecord ToEntity()
    {
        return new TagHistoryRecord
        {
            TagName = this.TagName,
            Timestamp = this.Timestamp,
            TagValue = this.TagValue
        };
    }
}