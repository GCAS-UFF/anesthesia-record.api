using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UFF.FichaAnestesica.Application.Interfaces;
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
        private readonly ISurgeryService _surgeryService;

        private readonly IRazorViewRenderer _razorViewRenderer;
        private readonly IPdfService _pdfService;
        private readonly ILogger<AnesthesiaRecordController> _logger;

        public AnesthesiaRecordController(IAnesthesiaRecordService anesthesiaRecordService, IRazorViewRenderer razorViewRenderer,
            IPdfService pdfService, ISurgeryService surgeryService, ILogger<AnesthesiaRecordController> logger)
        {
            _anesthesiaRecordService = anesthesiaRecordService;

            _razorViewRenderer = razorViewRenderer;
            _pdfService = pdfService;
            _surgeryService = surgeryService;
            _logger = logger;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] AnesthesiaRecordCommand command)
        {
            var response = await _anesthesiaRecordService.Create(command);

            if (!response.Valid)
                return BadRequest(response.Data);

            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Updadate([FromRoute] int id, [FromBody] AnesthesiaRecordCommand command)
        {
            var result = await _anesthesiaRecordService.Update(id, command);

            if (!result.Valid)
                return result.Forbidden
                    ? StatusCode(403, new CommandResult(false, result.Data, result.Message) { Forbidden = true })
                    : BadRequest(new CommandResult(false, result.Data));

            return Ok(new CommandResult(true, result));
        }

        [HttpGet("{id}/{extenalPatientId}")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, string extenalPatientId)
        {
            var surgeries = await _anesthesiaRecordService.GetByIdAsync(id, extenalPatientId);
            return Ok(surgeries);
        }

        [HttpPatch("{id}/reopen")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Reopen([FromRoute] int id)
        {
            var result = await _anesthesiaRecordService.Reopen(id);

            if (!result.Valid)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("my-patients")]
        public async Task<IActionResult> GetMyPatients([FromQuery] int doctorId, [FromQuery] DateTime? date, [FromQuery] string? term, [FromQuery] SurgeryStatusEnum? status, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _surgeryService.GetMyPatientsAsync(doctorId, date, term, status, page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}/print")]
        public async Task<IActionResult> Print([FromRoute] int id)
        {
            _logger.LogInformation("[PDF] Endpoint /print acionado para a ficha {Id}.", id);

            (string html, string extenalPatientId) = await _pdfService.GeneratePdfAsync(id);

            _logger.LogInformation("[PDF] Endpoint /print finalizado para a ficha {Id}, enviando response.", id);

            return Content(html, "text/html");
        }
    }
}
