using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using scada_back.Tag.Model.Abstraction;

namespace scada_back.Tag.Model;

public class DigitalOutputTag : IDigitalTag, IOutputTag
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("tag_name")]
    public string TagName { get; set; }
    [BsonElement("tag_type")]
    public string TagType { get; set; } = "output";
    [BsonElement("signal_type")]
    public string SignalType { get; set; } = "digital";
    [BsonElement("description")]
    public string Description { get; set; }
    [BsonElement("io_address")]
    public string IOAddress { get; set; }
    [BsonElement("initial_value")]
    public double InitialValue { get; set; }
}