namespace scada_back.User;

public interface IUserService
{
    string Login(string username, string password);
}