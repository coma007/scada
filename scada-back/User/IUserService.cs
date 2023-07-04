namespace scada_back.User;

public interface IUserService
{
    UserDTO Login(string username, string password);
}