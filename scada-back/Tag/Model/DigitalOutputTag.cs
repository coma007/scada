using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using scada_back.Tag.Model.Abstraction;

namespace scada_back.Tag.Model;

[BsonDiscriminator("digital_output")]
public class DigitalOutputTag : Abstraction.Tag, IDigitalTag, IOutputTag
{
    [BsonElement("initial_value")]
    public double InitialValue { get; set; }

    public override TagDto ToDto()
    {
        return new DigitalOutputTagDto
        {
            TagName = this.TagName,
            TagType = "digital_output",
            Description = this.Description,
            IOAddress = this.IOAddress,
            InitialValue = this.InitialValue
        };
    }
    
}

public class DigitalOutputTagDto :  TagDto, IDigitalTagDto, IOutputTagDto
{
    public double InitialValue { get; set; }
    public override Tag.Model.Abstraction.Tag ToEntity()
    {
        return new DigitalOutputTag
        {
            TagName = this.TagName,
            Description = this.Description,
            IOAddress = this.IOAddress,
            InitialValue = this.InitialValue
        };
    }

    public DigitalOutputTagDto()
    {
        
    }

    [JsonConstructor]
    public DigitalOutputTagDto(string tagName, string tagType, string description, int ioAddress, double initialValue) : base(tagName, tagType, description, ioAddress)
    {
        InitialValue = initialValue;
    }
}