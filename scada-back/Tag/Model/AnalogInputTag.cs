using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using scada_back.Tag.Model.Abstraction;

namespace scada_back.Tag.Model;

public class AnalogInputTag : IAnalogTag, IInputTag
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("tag_name")]
    public string TagName { get; set; }
    [BsonElement("tag_type")]
    public string TagType { get; set; } = "input";
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
    [BsonElement("driver")]
    public object Driver { get; set; }
    [BsonElement("scan_time")]
    public double ScanTime { get; set; }
    [BsonElement("scan")]
    public bool Scan { get; set; }

    public AnalogInputTagDTO ToDTO()
    {
        return new AnalogInputTagDTO
        {
            TagName = this.TagName,
            TagType = this.TagType,
            SignalType = this.SignalType,
            Description = this.Description,
            IOAddress = this.IOAddress,
            LowLimit = this.LowLimit,
            HightLimit = this.HightLimit,
            Units = this.Units,
            Driver = this.Driver,
            ScanTime = this.ScanTime,
            Scan = this.Scan
        };
    }
}

public class AnalogInputTagDTO : IAnalogTagDTO, IInputTagDTO
{
    public string TagName { get; set; }
    public string TagType { get; set; }
    public string SignalType { get; set; }
    public string Description { get; set; }
    public string IOAddress { get; set; }
    public double LowLimit { get; set; }
    public double HightLimit { get; set; }
    public string Units { get; set; }
    public object Driver { get; set; }
    public double ScanTime { get; set; }
    public bool Scan { get; set; }
}