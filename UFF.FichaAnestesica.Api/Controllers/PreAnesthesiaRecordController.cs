using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UFF.FichaAnestesica.Application.Interfaces;
using UFF.FichaAnestesica.Domain.Commands.PreAnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PreAnesthesiaRecordController : ControllerBase
    {
        private readonly IPreAnesthesiaRecordService _preAnesthesiaRecordService;
        private readonly IPreAnesthesiaRecordPrintService _printService;
        private readonly IRazorViewRenderer _razorViewRenderer;
        private readonly ILogger<PreAnesthesiaRecordController> _logger;

        private const string PrintViewPath = "~/Views/Pdf/PreAnesthesiaRecord.cshtml";

        public PreAnesthesiaRecordController(
            IPreAnesthesiaRecordService preAnesthesiaRecordService,
            IPreAnesthesiaRecordPrintService printService,
            IRazorViewRenderer razorViewRenderer,
            ILogger<PreAnesthesiaRecordController> logger)
        {
            _preAnesthesiaRecordService = preAnesthesiaRecordService;
            _printService = printService;
            _razorViewRenderer = razorViewRenderer;
            _logger = logger;
        }

    
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _preAnesthesiaRecordService.GetByIdAsync(id);
            if (!result.Valid)
                return NotFound(result);
            return Ok(result);
        }

        [HttpGet("by-anesthesia-record/{anesthesiaRecordId}")]
        [Authorize]
        public async Task<IActionResult> GetByAnesthesiaRecordId([FromRoute] int anesthesiaRecordId)
        {
            var result = await _preAnesthesiaRecordService.GetByAnesthesiaRecordIdAsync(anesthesiaRecordId);
            if (!result.Valid)
                return NotFound(result);
            return Ok(result);
        }

     
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] PreAnesthesiaRecordCommand command)
        {
            var result = await _preAnesthesiaRecordService.Create(command);
            if (!result.Valid)
                return result.Forbidden ? StatusCode(403, result) : BadRequest(result);
            return Created(string.Empty, result);
        }


        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PreAnesthesiaRecordCommand command)
        {
            if (command == null)
                return BadRequest(command);

            var result = await _preAnesthesiaRecordService.Update(id, command);
            if (!result.Valid)
                return result.Forbidden ? StatusCode(403, result) : BadRequest(result);
            return Ok(result);
        }

        [HttpGet("by-anesthesia-record/{anesthesiaRecordId}/print")]
        public async Task<IActionResult> Print([FromRoute] int anesthesiaRecordId)
        {
            _logger.LogInformation("[PDF] Endpoint /print acionado para a avaliação pré-anestésica da ficha {Id}.", anesthesiaRecordId);

            var viewModel = await _printService.BuildAsync(anesthesiaRecordId);

            if (viewModel == null)
            {
                _logger.LogWarning("[PDF] Avaliação pré-anestésica não encontrada para a ficha {Id} — abortando impressão.", anesthesiaRecordId);
                return NotFound();
            }

            var html = await _razorViewRenderer.RenderAsync(PrintViewPath, viewModel);

            _logger.LogInformation("[PDF] Endpoint /print finalizado para a avaliação pré-anestésica da ficha {Id}, enviando response.", anesthesiaRecordId);

            return Content(html, "text/html");
        }
    }
}
