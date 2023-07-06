using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using scada_back.Alarm.Enumeration;

namespace scada_back.DriverState;

public class DriverState
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
    [BsonElement("io_address")]
    public int IOAddress { get; set; }
    [BsonElement("value")]
    public double Value { get; set; }

    public DriverStateDTO ToDto()
    {
        return new DriverStateDTO()
        {
            Value = Value,
            IOAddress = IOAddress
        };
    }
}

public class DriverStateDTO
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