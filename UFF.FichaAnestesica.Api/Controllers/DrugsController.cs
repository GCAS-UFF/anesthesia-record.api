using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Api.Controllers
{
    [ApiController]
    [Route("api/drugs")]
    public class DrugsController : ControllerBase
    {
        private readonly IDrugService _drugService;

        public DrugsController(IDrugService drugService)
        {
            _drugService = drugService;
        }

        [HttpGet()]
        [Authorize]
        public async Task<IActionResult> GetAllDrugsForAnethesiaRecord()
        {
            var drugs = await _drugService.GetAllDrugsForAnethesiaRecord();
            return Ok(drugs);
        }
    }
}