using Microsoft.AspNetCore.Mvc;
using UFF.FichaAnestesica.AppService.Services;
using UFF.FichaAnestesica.Domain.Dto;

namespace UFF.FichaAnestesica.Api.Controllers
{
    [ApiController]
    [Route("api/surgeries")]
    public class SurgeriesController : ControllerBase
    {
        private readonly SurgeriesAppService _surgeriesAppService;

        public SurgeriesController(SurgeriesAppService surgeriesAppService)
        {
            _surgeriesAppService = surgeriesAppService;
        }

        [HttpGet("today")]
        public async Task<IActionResult> GetSurgeriesToday()
        {
            try
            {
                var mappedList = await _surgeriesAppService.SyncAndGetSurgeriesTodayAsync();
                return Ok(mappedList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to access or map data from HUAP.", details = ex.Message });
            }
        }
    }
}
