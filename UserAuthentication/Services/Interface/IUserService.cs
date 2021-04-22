using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAuthentication.Models.Request;
using UserAuthentication.Models.Response;

namespace UserAuthentication.Services.Interface
{
    public interface IUserService
    {
        string LogIn(UserCredRequest userCredRequest);
        int Register(UserRequest userRequest);
        UserResponse Get(int id);

    }
}
