using Microsoft.AspNetCore.Mvc;
using UFF.FichaAnestesica.Application.Interfaces;
using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnesthesiaRecordController : ControllerBase
    {
        private readonly IAnesthesiaRecordService _anesthesiaRecordService;

        private readonly IRazorViewRenderer _razorViewRenderer;
        private readonly IPdfService _pdfService;

        public AnesthesiaRecordController(IAnesthesiaRecordService anesthesiaRecordService, IRazorViewRenderer razorViewRenderer,
            IPdfService pdfService)
        {
            _anesthesiaRecordService = anesthesiaRecordService;

            _razorViewRenderer = razorViewRenderer;
            _pdfService = pdfService;
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

        [HttpGet("{id}/pdf")]
        public async Task<IActionResult> GeneratePdf([FromRoute] int id)
        {          
            (byte[] pdf, string extenalPatientId) = await _pdfService.GeneratePdfAsync(id);

            if (pdf == null)
                throw new Exception("Não foi possível gerar o PDF");

            return File(pdf, "application/pdf", $"ficha-anestesica-{extenalPatientId}.pdf");
        }       
    }
}
