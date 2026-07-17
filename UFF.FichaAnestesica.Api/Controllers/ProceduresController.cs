using Microsoft.AspNetCore.Mvc;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Api.Controllers
{
    [ApiController]
    [Route("api/procedures")]
    public class ProceduresController : ControllerBase
    {
        private readonly IProcedureService _procedureService;

        public ProceduresController(IProcedureService procedureService)
        {
            _procedureService = procedureService;
        }

        [HttpGet()]
        //    [Authorize]
        public async Task<IActionResult> GetAllProceduresForAnethesiaRecord()
        {
            var drugs = await _procedureService.GetAllProceduresForAnethesiaRecord();
            return Ok(drugs);
        }
    }
}