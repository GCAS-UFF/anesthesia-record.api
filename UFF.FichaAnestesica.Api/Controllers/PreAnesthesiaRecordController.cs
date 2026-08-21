using Microsoft.AspNetCore.Mvc;
using UFF.FichaAnestesica.Domain.Commands.PreAnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PreAnesthesiaRecordController : ControllerBase
    {
        private readonly IPreAnesthesiaRecordService _preAnesthesiaRecordService;

        public PreAnesthesiaRecordController(IPreAnesthesiaRecordService preAnesthesiaRecordService)
        {
            _preAnesthesiaRecordService = preAnesthesiaRecordService;
        }

    
        [HttpGet("{id}")]
        // [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _preAnesthesiaRecordService.GetByIdAsync(id);
            if (!result.Valid)
                return NotFound(result);
            return Ok(result);
        }

        [HttpGet("by-anesthesia-record/{anesthesiaRecordId}")]
        // [Authorize]
        public async Task<IActionResult> GetByAnesthesiaRecordId([FromRoute] int anesthesiaRecordId)
        {
            var result = await _preAnesthesiaRecordService.GetByAnesthesiaRecordIdAsync(anesthesiaRecordId);
            if (!result.Valid)
                return NotFound(result);
            return Ok(result);
        }

     
        [HttpPost]
        // [Authorize]
        public async Task<IActionResult> Create([FromBody] PreAnesthesiaRecordCommand command)
        {
            var result = await _preAnesthesiaRecordService.Create(command);
            if (!result.Valid)
                return BadRequest(result);
            return Created(string.Empty, result);
        }

        
        [HttpPut("{id}")]
        // [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PreAnesthesiaRecordCommand command)
        {
            if (command == null)
                return BadRequest(command);

            var result = await _preAnesthesiaRecordService.Update(id, command);
            if (!result.Valid)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
