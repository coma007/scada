using System.Text.Json;
using System.Text.Json.Serialization;
using scada_back.Tag.Model.Abstraction;

namespace scada_back.Tag.Model.Converter;

public class TagDtoConverter : JsonConverter<TagDto>
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(TagDto);
    }

    public override TagDto? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions? options)
    {
        var jsonObject = JsonDocument.ParseValue(ref reader).RootElement;
        var discriminatorValue = jsonObject.GetProperty("tagType").GetString();

        return discriminatorValue?.ToLower().Trim() switch
        {
            "analog_input" => JsonSerializer.Deserialize<AnalogInputTagDto>(jsonObject.GetRawText(), options),
            "analog_output" => JsonSerializer.Deserialize<AnalogOutputTagDto>(jsonObject.GetRawText(), options),
            "digital_input" => JsonSerializer.Deserialize<DigitalInputTagDto>(jsonObject.GetRawText(), options),
            "digital_output" => JsonSerializer.Deserialize<DigitalOutputTagDto>(jsonObject.GetRawText(), options),
            _ => throw new NotSupportedException($"Unknown discriminator value: {discriminatorValue}")
        };
    }

    public override void Write(Utf8JsonWriter writer, TagDto value, JsonSerializerOptions options)
    {
        var json = JsonSerializer.Serialize(value, value.GetType(), options);
        writer.WriteRawValue(json);
    }

}
