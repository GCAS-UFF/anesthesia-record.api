using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UFF.FichaAnestesica.Domain.Commands.EventTypes;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Api.Controllers
{
    [ApiController]
    [Route("api/event-types")]
    [Authorize]
    public class EventTypesController : ControllerBase
    {
        private readonly IEventTypeService _eventTypeService;

        public EventTypesController(IEventTypeService eventTypeService)
        {
            _eventTypeService = eventTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetActive()
        {
            var result = await _eventTypeService.GetActiveAsync();
            return Ok(result);
        }

        [HttpGet("admin")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetPagedForAdmin([FromQuery] string? term, [FromQuery] int page = 1, [FromQuery] int size = 20)
        {
            var result = await _eventTypeService.GetPagedForAdminAsync(term, page, size);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Create([FromBody] CreateEventTypeCommand command)
        {
            var result = await _eventTypeService.CreateAsync(command);

            if (!result.Valid)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateEventTypeCommand command)
        {
            var result = await _eventTypeService.UpdateAsync(id, command);

            if (!result.Valid)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
