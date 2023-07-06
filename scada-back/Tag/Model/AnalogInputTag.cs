using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using scada_back.Tag.Enumeration;
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
    public string Units { get; set; } = string.Empty;
    [BsonElement("driver")]
    public DriverType Driver { get; set; }
    [BsonElement("scan_time")]
    public double ScanTime { get; set; }
    [BsonElement("scan")]
    public bool Scan { get; set; }
    
    
    public override TagDto ToDto()
    {
        string driver = "SIMULATION";
        if (this.Driver == DriverType.REALTIME)
        {
            driver = "REALTIME";
        }
        return new AnalogInputTagDto
        {
            TagName = this.TagName,
            TagType = "analog_input",
            Description = this.Description,
            IOAddress = this.IOAddress,
            LowLimit = this.LowLimit,
            HighLimit = this.HighLimit,
            Units = this.Units,
            Driver = driver,
            ScanTime = this.ScanTime,
            Scan = this.Scan
        };
    }
    
}

public class AnalogInputTagDto :  TagDto, IAnalogTagDto, IInputTagDto
{
    public double LowLimit { get; set; }
    public double HighLimit { get; set; }
    public string Units { get; set; } = string.Empty;
    public string Driver { get; set; }
    public double ScanTime { get; set; }
    public bool Scan { get; set; }
    
    public override Tag.Model.Abstraction.Tag ToEntity()
    {
        DriverType driver = DriverType.SIMULATION;
        if (this.Driver.ToUpper() == "REALTIME")
        {
            driver = DriverType.REALTIME;
        }
        return new AnalogInputTag
        {
            TagName = this.TagName,
            Description = this.Description,
            IOAddress = this.IOAddress,
            LowLimit = this.LowLimit,
            HighLimit = this.HighLimit,
            Units = this.Units,
            Driver = driver,
            ScanTime = this.ScanTime,
            Scan = this.Scan
        };
    }

    public AnalogInputTagDto()
    {
        
    }

    [JsonConstructor]
    public AnalogInputTagDto(string tagName, string tagType, string description, int ioAddress, double lowLimit, double highLimit, string units, string? driver, double scanTime, bool scan) : base(tagName, tagType, description, ioAddress)
    {
        LowLimit = lowLimit;
        HighLimit = highLimit;
        Units = units;
        Driver = driver?.ToUpper();
        ScanTime = scanTime;
        Scan = scan;
    }
}