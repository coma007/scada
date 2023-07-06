using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using scada_back.Tag.Model.Abstraction;

namespace scada_back.Tag.Model;

[BsonDiscriminator("analog_output")]
public class AnalogOutputTag : Abstraction.Tag, IAnalogTag, IOutputTag
{
    [BsonElement("low_limit")]
    public double LowLimit { get; set; }
    [BsonElement("high_limit")]
    public double HighLimit { get; set; }
    [BsonElement("units")]
    public string Units { get; set; }  = string.Empty;
    [BsonElement("initial_value")]
    public double InitialValue { get; set; }
    
    public override TagDto ToDto()
    {
        return new AnalogOutputTagDto
        {
            TagName = this.TagName,
            TagType = "analog_output",
            Description = this.Description,
            IOAddress = this.IOAddress,
            LowLimit = this.LowLimit,
            HighLimit = this.HighLimit,
            Units = this.Units,
            InitialValue = this.InitialValue
        };
    }
    
}

public class AnalogOutputTagDto : TagDto, IAnalogTagDto, IOutputTagDto
{
    public double LowLimit { get; set; }
    public double HighLimit { get; set; }
    public string Units { get; set; } = string.Empty;
    public double InitialValue { get; set; }
    
    public override Tag.Model.Abstraction.Tag ToEntity()
    {
        return new AnalogOutputTag
        {
            TagName = this.TagName,
            Description = this.Description,
            IOAddress = this.IOAddress,
            LowLimit = this.LowLimit,
            HighLimit = this.HighLimit,
            Units = this.Units,
            InitialValue = this.InitialValue
        };
    }

    public AnalogOutputTagDto()
    {
        
    }

    [JsonConstructor]
    public AnalogOutputTagDto(string tagName, string tagType, string description, string ioAddress, double lowLimit, double highLimit, string units, double initialValue) : base(tagName, tagType, description, ioAddress)
    {
        LowLimit = lowLimit;
        HighLimit = highLimit;
        Units = units;
        InitialValue = initialValue;
    }
}