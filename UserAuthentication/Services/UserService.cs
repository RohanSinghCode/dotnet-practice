using UserAuthentication.Models.Request;
using UserAuthentication.Models.Response;
using UserAuthentication.Services.Interface;
using UserAuthentication.Models;
using UserAuthentication.Repository.Interface;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using System.Text;

namespace UserAuthentication.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly string _key;

        public UserService(IUserRepository userRepository, string key)
        {
            _userRepository = userRepository;
            _key = key;
        }
        public string LogIn(UserCredRequest userCredRequest)
        {
            var userCred = new UserCred
            {
                Username = userCredRequest.UserName,
                Password = userCredRequest.Password
            };
            var id = _userRepository.Authenticate(userCred).ToString();
            if (id == "0")
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var claims = new[]
            {
                new Claim("UserId",id)
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials
                                            (
                                            new SymmetricSecurityKey(tokenKey),
                                            SecurityAlgorithms.HmacSha256Signature
                                            )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public int Register(UserRequest userRequest)
        {
            var user = new User()
            {
                Username = userRequest.UserName,
                Password = userRequest.Password,
                Name = userRequest.Name
            };
            var id = _userRepository.Register(user);

            return id;
        }
        public UserResponse Get(int id)
        {
            var user = _userRepository.Get(id);
            return new UserResponse { Id = user.Id, UserName = user.Username, Name = user.Name };
        }
    }
}
