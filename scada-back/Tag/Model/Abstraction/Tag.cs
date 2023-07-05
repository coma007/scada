using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace scada_back.Tag.Model.Abstraction;

[BsonKnownTypes(typeof(AnalogInputTag), typeof(AnalogOutputTag), typeof(DigitalInputTag), typeof(DigitalOutputTag))]
public abstract class Tag
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("tag_name")]
    public string TagName { get; set; }
    [BsonElement("description")]
    public string Description { get; set; }
    [BsonElement("io_address")]
    public string IOAddress { get; set; }


    public abstract TagDTO ToDTO();
}

public abstract class TagDTO
{
    public string TagName { get; set; }
    public string TagType { get; set; }
    public string Description { get; set; }
    public string IOAddress { get; set; }

    public abstract Tag ToEntity();

    public TagDTO()
    {
        
    }
    
    [JsonConstructor]
    protected TagDTO(string tagName, string tagType, string description, string ioAddress)
    {
        TagName = tagName;
        TagType = tagType;
        Description = description;
        IOAddress = ioAddress;
    }
}