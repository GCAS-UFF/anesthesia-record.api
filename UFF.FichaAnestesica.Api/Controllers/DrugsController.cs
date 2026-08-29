using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UFF.FichaAnestesica.Domain.Commands.Drugs;
using UFF.FichaAnestesica.Domain.Enums;
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

        [HttpGet("admin")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetPagedForAdmin([FromQuery] string? term, [FromQuery] DrugCategoryEnum? category, [FromQuery] int page = 1, [FromQuery] int size = 20)
        {
            var result = await _drugService.GetPagedForAdminAsync(term, category, page, size);
            return Ok(result);
        }

        [HttpPut("{id}/category")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] UpdateDrugCategoryCommand command)
        {
            var result = await _drugService.UpdateCategoryAsync(id, command);

            if (!result.Valid)
                return BadRequest(result);

            return Ok(result);
        }
    }
}