using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using scada_back.Tag.Model.Abstraction;

namespace scada_back.Tag.Model;

public class DigitalInputTag : IDigitalTag, IInputTag
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("tag_name")]
    public string TagName { get; set; }
    [BsonElement("tag_type")]
    public string TagType { get; set; } = "input";
    [BsonElement("signal_type")]
    public string SignalType { get; set; } = "digital";
    [BsonElement("description")]
    public string Description { get; set; }
    [BsonElement("io_address")]
    public string IOAddress { get; set; }
    [BsonElement("driver")]
    public object Driver { get; set; }
    [BsonElement("scan_time")]
    public double ScanTime { get; set; }
    [BsonElement("scan")]
    public bool Scan { get; set; }
    
    public IAbstractTagDTO ToDTO()
    {
        return new DigitalInputTagDTO
        {
            TagName = this.TagName,
            TagType = this.TagType,
            SignalType = this.SignalType,
            Description = this.Description,
            IOAddress = this.IOAddress,
            Driver = this.Driver,
            ScanTime = this.ScanTime,
            Scan = this.Scan
        };
    }

}

public class DigitalInputTagDTO : IDigitalTagDTO, IInputTagDTO
{
    public string TagName { get; set; }
    public string TagType { get; set; } = "input";
    public string SignalType { get; set; } = "digital";
    public string Description { get; set; }
    public string IOAddress { get; set; }
    public object Driver { get; set; }
    public double ScanTime { get; set; }
    public bool Scan { get; set; }
}