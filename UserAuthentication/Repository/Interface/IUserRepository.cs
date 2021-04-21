using UserAuthentication.Models;

namespace UserAuthentication.Repository.Interface
{
    public interface IUserRepository
    {
        int Authenticate(UserCred userCred);
        int Register(User user);
        User Get(int id);

    }
}
