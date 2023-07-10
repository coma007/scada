using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using scada_back.Infrastructure.Feature.Tag.Enumeration;
using scada_back.Infrastructure.Feature.Tag.Model.Abstraction;

namespace scada_back.Infrastructure.Feature.Tag.Model;

[BsonDiscriminator("digital_input")]
public class DigitalInputTag : Abstraction.Tag, IDigitalTag, IInputTag
{
    [BsonElement("driver")]
    public DriverType Driver { get; set; }
    [BsonElement("scan_time")]
    public double ScanTime { get; set; }
    [BsonElement("scan")]
    public bool Scan { get; set; }
    
    public override TagDto ToDto()
    {
        string driver = "REALTIME";
        if (this.Driver == DriverType.Simulation)
        {
            driver = "SIMULATION";
        }
        return new DigitalInputTagDto
        {
            TagName = this.TagName,
            TagType = "digital_input",
            Description = this.Description,
            IOAddress = this.IOAddress,
            Driver = driver,
            ScanTime = this.ScanTime,
            Scan = this.Scan
        };
    }

}

public class DigitalInputTagDto :  TagDto, IDigitalTagDto, IInputTagDto
{
    public string Driver { get; set; } = string.Empty;
    public double ScanTime { get; set; }
    public bool Scan { get; set; }
    
    public override Tag.Model.Abstraction.Tag ToEntity()
    {
        DriverType driver = DriverType.Realtime;
        if (this.Driver.ToUpper() == "SIMULATION")
        {
            driver = DriverType.Simulation;
        }
        return new DigitalInputTag
        {
            TagName = this.TagName,
            Description = this.Description,
            IOAddress = this.IOAddress,
            Driver = driver,
            ScanTime = this.ScanTime,
            Scan = this.Scan
        };
    }

    public DigitalInputTagDto()
    {
        
    }

    [JsonConstructor]
    public DigitalInputTagDto(string tagName, string tagType, string description, int ioAddress, string driver, double scanTime, bool scan) : base(tagName, tagType, description, ioAddress)
    {
        Driver = driver.ToUpper();
        ScanTime = scanTime;
        Scan = scan;
    }
}