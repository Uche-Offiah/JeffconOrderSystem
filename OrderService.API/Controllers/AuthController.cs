using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.DTOs;
using OrderService.Application.Interfaces;

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
