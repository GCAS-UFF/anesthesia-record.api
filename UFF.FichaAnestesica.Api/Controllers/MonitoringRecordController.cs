using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonitoringRecordController : ControllerBase
    {
        private readonly IMonitoringRecordService _monitoringRecordService;

        public MonitoringRecordController(IMonitoringRecordService monitoringRecordService)
        {
            _monitoringRecordService = monitoringRecordService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _monitoringRecordService.GetByIdAsync(id);

            if (!result.Valid)
                return result.Forbidden ? StatusCode(403, result) : NotFound(result);

            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] MonitoringRecordCommand command)
        {
            var result = await _monitoringRecordService.Create(command);

            if (!result.Valid)
                return BadRequest(result);

            return Created(string.Empty, result);
        }

      
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] MonitoringRecordCommand command)
        {
            if (command == null)
                return BadRequest(command);

            var result = await _monitoringRecordService.Update(id, command);

            if (!result.Valid)
                return result.Forbidden ? StatusCode(403, result) : BadRequest(result);

            return Ok(result);
        }

 
        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> FinalizePatientAsync([FromRoute] int id, [FromBody] MonitoringRecordCommand? command)
        {
            var result = await _monitoringRecordService.FinalizePatientAsync(id, command);

            if (!result.Valid)
                return result.Forbidden ? StatusCode(403, result) : BadRequest(result);

            return Ok(result);
        }
    }
}