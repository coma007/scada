using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using scada_back.Tag.Model.Abstraction;

namespace scada_back.Tag.Model;

public class AnalogOutputTag :IAnalogTag, IOutputTag
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("tag_name")]
    public string TagName { get; set; }
    [BsonElement("tag_type")] 
    public string TagType { get; set; } = "output";
    [BsonElement("signal_type")]
    public string SignalType { get; set; } = "analog";
    [BsonElement("description")]
    public string Description { get; set; }
    [BsonElement("io_address")]
    public string IOAddress { get; set; }
    [BsonElement("low_limit")]
    public double LowLimit { get; set; }
    [BsonElement("high_limit")]
    public double HightLimit { get; set; }
    [BsonElement("units")]
    public string Units { get; set; }
    [BsonElement("initial_value")]
    public double InitialValue { get; set; }
}
