namespace scada_back.Infrastructure.Feature.User;

public interface IUserService
{
    string Login(string username, string password);
}