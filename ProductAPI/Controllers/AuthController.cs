using Microsoft.AspNetCore.Mvc;
using ProductAPI.Helpers;
using ProductAPI.Models.ProductApi.Models;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin login)
        {
            if (login.Username == "taller-admin" && login.Password == "123456")
            {
                var accessToken = _jwtService.GenerateToken(login.Username);
                return Ok(new { accessToken });
            }

            return Unauthorized();
        }
    }
}
