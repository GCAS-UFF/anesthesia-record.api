using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.Reports;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Api.Controllers
{
    [ApiController]
    [Route("api/reports")]
    [Authorize(Policy = "AdminOnly")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IReportPdfService _reportPdfService;

        public ReportsController(IReportService reportService, IReportPdfService reportPdfService)
        {
            _reportService = reportService;
            _reportPdfService = reportPdfService;
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary([FromQuery] ReportFilterQuery filter)
        {
            var result = await _reportService.GetSummaryAsync(filter);
            return result.Valid ? Ok(result) : BadRequest(result);
        }

        [HttpGet("clinical-events")]
        public async Task<IActionResult> GetClinicalEvents([FromQuery] ReportFilterQuery filter)
        {
            var result = await _reportService.GetClinicalEventsAsync(filter);
            return result.Valid ? Ok(result) : BadRequest(result);
        }

        [HttpGet("drug-consumption")]
        public async Task<IActionResult> GetDrugConsumption([FromQuery] ReportFilterQuery filter, [FromQuery] DrugCategoryEnum? category)
        {
            var result = await _reportService.GetDrugConsumptionAsync(filter, category);
            return result.Valid ? Ok(result) : BadRequest(result);
        }

        [HttpGet("surgeries")]
        public async Task<IActionResult> GetSurgeries([FromQuery] ReportFilterQuery filter)
        {
            var result = await _reportService.GetSurgeriesAsync(filter);
            return result.Valid ? Ok(result) : BadRequest(result);
        }

        [HttpGet("anesthetists")]
        public async Task<IActionResult> GetAnesthetists([FromQuery] ReportFilterQuery filter)
        {
            var result = await _reportService.GetAnesthetistsAsync(filter);
            return result.Valid ? Ok(result) : BadRequest(result);
        }

        [HttpGet("cancellations")]
        public async Task<IActionResult> GetCancellations([FromQuery] ReportFilterQuery filter)
        {
            var result = await _reportService.GetCancellationsAsync(filter);
            return result.Valid ? Ok(result) : BadRequest(result);
        }

        [HttpGet("asa")]
        public async Task<IActionResult> GetAsa([FromQuery] ReportFilterQuery filter)
        {
            var result = await _reportService.GetAsaAsync(filter);
            return result.Valid ? Ok(result) : BadRequest(result);
        }

        [HttpGet("recovery")]
        public async Task<IActionResult> GetRecovery([FromQuery] ReportFilterQuery filter)
        {
            var result = await _reportService.GetRecoveryAsync(filter);
            return result.Valid ? Ok(result) : BadRequest(result);
        }

        [HttpGet("antibiotic-prophylaxis")]
        public async Task<IActionResult> GetAntibioticProphylaxis([FromQuery] ReportFilterQuery filter)
        {
            var result = await _reportService.GetAntibioticProphylaxisAsync(filter);
            return result.Valid ? Ok(result) : BadRequest(result);
        }

        [HttpGet("fluid-balance")]
        public async Task<IActionResult> GetFluidBalance([FromQuery] ReportFilterQuery filter)
        {
            var result = await _reportService.GetFluidBalanceAsync(filter);
            return result.Valid ? Ok(result) : BadRequest(result);
        }

        [HttpGet("integration-status")]
        public async Task<IActionResult> GetIntegrationStatus()
        {
            var result = await _reportService.GetIntegrationStatusAsync();
            return result.Valid ? Ok(result) : BadRequest(result);
        }

        [HttpGet("filters/anesthetists")]
        public async Task<IActionResult> GetAnesthetistOptions()
        {
            var result = await _reportService.GetAnesthetistOptionsAsync();
            return result.Valid ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{reportKey}/pdf")]
        public async Task<IActionResult> GetPdf([FromRoute] string reportKey, [FromQuery] ReportFilterQuery filter, [FromQuery] DrugCategoryEnum? category)
        {
            var (bytes, error) = await _reportPdfService.GenerateAsync(reportKey, filter, category);

            if (error != null)
                return BadRequest(CommandResult.Fail(error));

            if (bytes == null)
                return NotFound(CommandResult.Fail("Relatório não encontrado."));

            var fileName = $"relatorio-{reportKey}-{DateTime.Now:yyyyMMdd-HHmm}.pdf";
            return File(bytes, "application/pdf", fileName);
        }
    }
}
