using Microsoft.AspNetCore.Mvc;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Api.Controllers
{
    [ApiController]
    [Route("api/professionals")]
    public class ProfessionalsController : ControllerBase
    {
        private readonly IProfessionalService _professionalService;

        public ProfessionalsController(IProfessionalService professionalService)
        {
            _professionalService = professionalService;
        }

        [HttpGet("{name}")]
        //    [Authorize]
        public async Task<IActionResult> GetProfessionalsByName([FromRoute] string name)
        {
            var professionals = await _professionalService.GetProfessionalsForAnethesiaRecord(name);
            return Ok(professionals);
        }
    }
}