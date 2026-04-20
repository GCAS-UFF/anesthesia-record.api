using Microsoft.AspNetCore.Mvc;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Api.Controllers
{
    [ApiController]
    [Route("api/surgeries")]
    public class SurgeriesController : ControllerBase
    {
        private readonly ISurgeryService _surgeriesAppService;

        public SurgeriesController(ISurgeryService surgeriesAppService)
        {
            _surgeriesAppService = surgeriesAppService;
        }

        [HttpGet("{date}/{status}")]
        public async Task<IActionResult> GetSurgeriesToday([FromRoute] DateTime date, [FromRoute] SurgeryStatus status)
        {
            var mappedList = await _surgeriesAppService.GetSurgeriesAsync(date, status);
            return Ok(mappedList);
        }
    }
}