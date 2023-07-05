namespace scada_back.User;

public interface IUserService
{
    UserDto Login(string username, string password);
}