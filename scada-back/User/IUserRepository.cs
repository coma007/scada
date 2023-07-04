namespace scada_back.User;

public interface IUserRepository
{
    User Get(string username);
}