using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{term}")]
        [Authorize]
        public async Task<IActionResult> GetProfessionalsByName([FromRoute] string term)
        {
            var professionals = await _professionalService.GetProfessionalsForAnethesiaRecord(term);
            return Ok(professionals);
        }

        [HttpGet()]
        [Authorize]
        public async Task<IActionResult> GetAllProceduresForAnethesiaRecord()
        {
            var professionals = await _professionalService.GetAllProfessionalsForAnethesiaRecord();
            return Ok(professionals);
        }
    }
}