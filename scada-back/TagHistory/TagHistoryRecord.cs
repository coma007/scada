using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace scada_back.TagHistory;

public class TagHistoryRecord
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
    [BsonElement("tag_name")]
    public string TagName { get; set; } = string.Empty;
    [BsonElement("timestamp")]
    public DateTime Timestamp { get; set; }
    [BsonElement("tag_value")]
    public Double TagValue { get; set; }

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
    public Double TagValue { get; set; }

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