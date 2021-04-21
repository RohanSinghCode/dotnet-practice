using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserAuthentication.Models.Request;
using UserAuthentication.Services.Interface;

namespace UserAuthentication.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var usertokken = HttpContext.User;
            var id = usertokken.Claims.FirstOrDefault(c => c.Type == "UserId").Value;
            var user = _userService.Get(Convert.ToInt32(id));
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserCredRequest userCredRequest)
        {
            var token = _userService.LogIn(userCredRequest);
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserRequest userRequest)
        {
            var id = _userService.Register(userRequest);

            return Ok(id);

        }
    }
}
