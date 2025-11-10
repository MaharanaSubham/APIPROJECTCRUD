using APICRUD.Helpers;
using APICRUD.Model;
using Microsoft.AspNetCore.Mvc;

namespace APICRUD.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private static List<UserDto> users = new List<UserDto>(); // In-memory users
        private readonly JwtService jwtService;

        public AuthController(JwtService jwtService)
        {
            this.jwtService = jwtService;
        }

        [HttpPost("register")]
        public IActionResult Register(UserDto request)
        {
            users.Add(request);
            return Ok("User Registered Successfully");
        }

        [HttpPost("login")]
        public IActionResult Login(UserDto request)
        {
            var user = users.FirstOrDefault(u =>
                u.Username == request.Username &&
                u.Password == request.Password);

            if (user == null)
                return Unauthorized("Invalid Credentials");

            var token = jwtService.GenerateToken(request.Username);
            return Ok(new { Token = token });
        }
    }
}
