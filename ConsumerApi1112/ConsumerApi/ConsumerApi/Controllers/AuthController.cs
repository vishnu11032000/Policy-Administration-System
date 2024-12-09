// AuthController.cs
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ConsumerApi.Models;
using ConsumerApi.Services;
using Microsoft.Extensions.Logging;

namespace ConsumerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AgentRegister model)
        {
            try
            {
                var agent = await _authService.RegisterAsync(model);
                return CreatedAtAction(nameof(Register), new { id = agent.AgentId }, agent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AgentLogin model)
        {
            try
            {
                var token = await _authService.LoginAsync(model);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid credentials.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
    }
}
