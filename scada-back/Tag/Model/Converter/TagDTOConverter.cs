using System.Text.Json;
using System.Text.Json.Serialization;
using scada_back.Tag.Model.Abstraction;

namespace scada_back.Tag.Model.Converter;

public class TagDTOConverter : JsonConverter<TagDTO>
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(TagDTO);
    }

    public override TagDTO? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions? options)
    {
        var jsonObject = JsonDocument.ParseValue(ref reader).RootElement;
        var discriminatorValue = jsonObject.GetProperty("tagType").GetString();

        return discriminatorValue switch
        {
            "analog_input" => JsonSerializer.Deserialize<AnalogInputTagDTO>(jsonObject.GetRawText(), options),
            "analog_output" => JsonSerializer.Deserialize<AnalogOutputTagDTO>(jsonObject.GetRawText(), options),
            "digital_input" => JsonSerializer.Deserialize<DigitalInputTagDTO>(jsonObject.GetRawText(), options),
            "digital_output" => JsonSerializer.Deserialize<DigitalOutputTagDTO>(jsonObject.GetRawText(), options),
            _ => throw new NotSupportedException($"Unknown discriminator value: {discriminatorValue}")
        };
    }

    public override void Write(Utf8JsonWriter writer, TagDTO value, JsonSerializerOptions options)
    {
        var json = JsonSerializer.Serialize(value, value.GetType(), options);
        writer.WriteRawValue(json);
    }

}
