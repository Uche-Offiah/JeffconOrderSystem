using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OrderService.Application.DTOs;
using OrderService.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrderService.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly IOrderAuth _userService;

        public AuthController(IConfiguration configuration, ILogger<AuthController> logger, IOrderAuth userService)
        {
            _configuration = configuration;
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: new[] { new Claim("userId", "123") },
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );


            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
           });
        }

        [HttpPost]
        [Route("VerifyUser")]
        public async Task<ActionResult> Login([FromBody] VerifyUser request)
        {
            var response = await _userService.Login(request);
            if (response == null)
            {
                return BadRequest(response);
            }
            else
            {
                return Content(response);
            }
        }

    }
}
