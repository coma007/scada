using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using scada_back.Tag.Model.Abstraction;

namespace scada_back.Tag.Model;

[BsonDiscriminator("digital_output")]
public class DigitalOutputTag : Abstraction.Tag, IDigitalTag, IOutputTag
{
    [BsonElement("initial_value")]
    public double InitialValue { get; set; }

    public override TagDTO ToDTO()
    {
        return new DigitalOutputTagDTO
        {
            TagName = this.TagName,
            TagType = "digital_output",
            Description = this.Description,
            IOAddress = this.IOAddress,
            InitialValue = this.InitialValue
        };
    }
    
}

public class DigitalOutputTagDTO :  TagDTO, IDigitalTagDTO, IOutputTagDTO
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

    public DigitalOutputTagDTO()
    {
        
    }

    [JsonConstructor]
    public DigitalOutputTagDTO(string tagName, string tagType, string description, string ioAddress, double initialValue) : base(tagName, tagType, description, ioAddress)
    {
        InitialValue = initialValue;
    }
}