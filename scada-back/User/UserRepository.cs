using MongoDB.Driver;
using scada_back.Database;

namespace scada_back.User;

public class UserRepository : IUserRepository
{
    private IMongoCollection<User> _users;

    public UserRepository(IScadaDatabaseSettings settings, IMongoClient mongoClient)
    {
        var _database = mongoClient.GetDatabase(settings.DatabaseName);
        _users = _database.GetCollection<User>(settings.UsersCollectionName);
    }
  
    public User Get(string username)
    {
        return _users.Find(user => user.Username == username).FirstOrDefault();
    }
}