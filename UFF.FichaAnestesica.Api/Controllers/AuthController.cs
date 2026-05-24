using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UFF.FichaAnestesica.Domain.Commands.Auth;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginCommand request)
        {
            var result = await _authService.AuthSync(request.Email, request.Password);

            if (!result.Valid)
                return Unauthorized(new { message = result.Data });

            return Ok(result);
        }
    }
}
