using Microsoft.AspNetCore.Mvc;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Api.Controllers
{
    [ApiController]
    [Route("api/surgeries")]
    public class SurgeriesController : ControllerBase
    {
        private readonly ISurgeryService _surgeriesService;

        public SurgeriesController(ISurgeryService surgeriesService)
        {
            _surgeriesService = surgeriesService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSurgeries([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var mappedList = await _surgeriesService.GetPatientsWithSurgeriesAsync(null, null, page, size);
            return Ok(mappedList);
        }

        [HttpGet("date/{date}")]
        public async Task<IActionResult> GetSurgeriesByDate([FromRoute] DateTime date, [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var mappedList = await _surgeriesService.GetPatientsWithSurgeriesAsync(date, null, page, size);
            return Ok(mappedList);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetSurgeriesByStatus([FromRoute] SurgeryStatus status, [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var mappedList = await _surgeriesService.GetPatientsWithSurgeriesAsync(null, status, page, size);
            return Ok(mappedList);
        }

        [HttpGet("date/{date}/status/{status}")]
        public async Task<IActionResult> GetSurgeriesByDateAndStatus([FromRoute] DateTime date, [FromRoute] SurgeryStatus status, [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var mappedList = await _surgeriesService.GetPatientsWithSurgeriesAsync(date, status, page, size);
            return Ok(mappedList);
        }
    }
}