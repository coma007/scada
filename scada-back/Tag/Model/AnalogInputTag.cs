using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using scada_back.Tag.Model.Abstraction;

namespace scada_back.Tag.Model;

[BsonDiscriminator("analog_input")]
public class AnalogInputTag : Abstraction.Tag, IAnalogTag, IInputTag
{
    [BsonElement("low_limit")]
    public double LowLimit { get; set; }
    [BsonElement("high_limit")]
    public double HighLimit { get; set; }
    [BsonElement("units")]
    public string Units { get; set; }
    [BsonElement("driver")]
    public string Driver { get; set; }
    [BsonElement("scan_time")]
    public double ScanTime { get; set; }
    [BsonElement("scan")]
    public bool Scan { get; set; }
    
    
    public override TagDTO ToDTO()
    {
        return new AnalogInputTagDTO
        {
            TagName = this.TagName,
            TagType = "analog_input",
            Description = this.Description,
            IOAddress = this.IOAddress,
            LowLimit = this.LowLimit,
            HighLimit = this.HighLimit,
            Units = this.Units,
            Driver = this.Driver,
            ScanTime = this.ScanTime,
            Scan = this.Scan
        };
    }
    
}

public class AnalogInputTagDTO :  TagDTO, IAnalogTagDTO, IInputTagDTO
{
    public double LowLimit { get; set; }
    public double HighLimit { get; set; }
    public string Units { get; set; }
    public string Driver { get; set; }
    public double ScanTime { get; set; }
    public bool Scan { get; set; }
    public override Tag.Model.Abstraction.Tag ToEntity()
    {
        return new AnalogInputTag
        {
            TagName = this.TagName,
            Description = this.Description,
            IOAddress = this.IOAddress,
            LowLimit = this.LowLimit,
            HighLimit = this.HighLimit,
            Units = this.Units,
            Driver = this.Driver,
            ScanTime = this.ScanTime,
            Scan = this.Scan
        };
    }

    public AnalogInputTagDTO()
    {
        
    }

    [JsonConstructor]
    public AnalogInputTagDTO(string tagName, string tagType, string description, string ioAddress, double lowLimit, double highLimit, string units, string driver, double scanTime, bool scan) : base(tagName, tagType, description, ioAddress)
    {
        LowLimit = lowLimit;
        HighLimit = highLimit;
        Units = units;
        Driver = driver;
        ScanTime = scanTime;
        Scan = scan;
    }
}