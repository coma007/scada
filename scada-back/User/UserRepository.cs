using MongoDB.Driver;
using scada_back.Database;

namespace scada_back.User;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _users;

    public UserRepository(IScadaDatabaseSettings settings, IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(settings.DatabaseName);
        _users = database.GetCollection<User>(settings.UsersCollectionName);
    }
  
    public User Get(string username)
    {
        return _users.Find(user => user.Username == username).FirstOrDefault();
    }
}