using scada_back.Exception;

namespace scada_back.User;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }
    public UserDto Login(string username, string password)
    {
        User user = _repository.Get(username);
        if (user == null || user.Password != password)
        {
            throw new ObjectNotFoundException("Incorrect credentials.");
        }
        return user.ToDto();
    }
}