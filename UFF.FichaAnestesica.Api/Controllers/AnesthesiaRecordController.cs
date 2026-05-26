using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnesthesiaRecordController : ControllerBase
    {
        private readonly IAnesthesiaRecordService _anesthesiaRecordService;

        public AnesthesiaRecordController(IAnesthesiaRecordService anesthesiaRecordService)
        {
            _anesthesiaRecordService = anesthesiaRecordService;
        }

        [HttpPost]
        // [Authorize]
        public async Task<IActionResult> Create([FromBody] AnesthesiaRecordCommand command)
        {
            var response = await _anesthesiaRecordService.Create(command);

            if (!response.Valid)
                return BadRequest(response.Data);

            return Ok(response);
        }

        [HttpPut("{id}")]
        // [Authorize]
        public async Task<IActionResult> Updadate([FromRoute] int id, [FromBody] AnesthesiaRecordCommand command)
        {
            var result = await _anesthesiaRecordService.Update(id, command);

            if (!result.Valid)
                return BadRequest(new CommandResult(false, result.Data));

            return Ok(new CommandResult(true, result));
        }

        [HttpGet("{id}")]
        //    [Authorize]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var surgeries = await _anesthesiaRecordService.GetByIdAsync(id);
            return Ok(surgeries);
        }
    }
}
