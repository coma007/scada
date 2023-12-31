using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace scada_back.Infrastructure.Feature.Tag.Model.Abstraction;

[BsonKnownTypes(typeof(AnalogInputTag), typeof(AnalogOutputTag), typeof(DigitalInputTag), typeof(DigitalOutputTag))]
public abstract class Tag
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
    [BsonElement("tag_name")]
    public string TagName { get; set; } = string.Empty;
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;
    [BsonElement("io_address")]
    public int IOAddress { get; set; }

    public abstract TagDto ToDto();
}

public abstract class TagDto
{
    public string TagName { get; set; } = string.Empty;
    public string TagType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int IOAddress { get; set; }

    public abstract Tag ToEntity();

    public TagDto()
    {
        
    }
    
    [JsonConstructor]
    protected TagDto(string tagName, string tagType, string description, int ioAddress)
    {
        TagName = tagName;
        TagType = tagType;
        Description = description;
        IOAddress = ioAddress;
    }
}