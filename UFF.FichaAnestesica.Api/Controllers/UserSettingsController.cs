using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UFF.FichaAnestesica.Domain.Commands.UserSettings;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserSettingsController : ControllerBase
    {
        private readonly IUserSettingsService _userSettingsService;

        public UserSettingsController(IUserSettingsService userSettingsService)
        {
            _userSettingsService = userSettingsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _userSettingsService.GetForCurrentUserAsync();

            if (!result.Valid)
                return Unauthorized(result);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserSettingsCommand command)
        {
            var result = await _userSettingsService.UpdateUserSettingsAsync(command);

            if (!result.Valid)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("institution")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateInstitution([FromBody] InstitutionSettingsCommand command)
        {
            var result = await _userSettingsService.UpdateInstitutionSettingsAsync(command);

            if (!result.Valid)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("admin-password")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> ChangeAdminPassword([FromBody] ChangeAdminPasswordCommand command)
        {
            var result = await _userSettingsService.ChangeAdminPasswordAsync(command);

            if (!result.Valid)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
