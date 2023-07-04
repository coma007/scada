using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace scada_back.User;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
    [BsonElement("username")]
    public string Username { get; set; } = string.Empty;
    [BsonElement("password")]
    public string Password { get; set; } = string.Empty;

    public UserDTO ToDTO()
    {
        return new UserDTO
        {
            Username = this.Username
        };
    }
}


public class UserDTO
{
    public String Username { get; set; } = String.Empty;

    
}