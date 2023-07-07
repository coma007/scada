using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace scada_back.Infrastructure.Feature.DriverState;

public class DriverState
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
    [BsonElement("io_address")]
    public int IOAddress { get; set; }
    [BsonElement("value")]
    public double Value { get; set; }

    public DriverStateDto ToDto()
    {
        return new DriverStateDto()
        {
            Value = Value,
            IOAddress = IOAddress
        };
    }
}

public class DriverStateDto
{
    public int IOAddress { get; set; }
    public double Value { get; set; }

    public DriverState ToEntity()
    {
        return new DriverState()
        {
            Value = Value,
            IOAddress = IOAddress
        };
    }
}