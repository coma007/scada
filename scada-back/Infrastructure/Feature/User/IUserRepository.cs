namespace scada_back.Infrastructure.Feature.User;

public interface IUserRepository
{
    User Get(string username);
}