using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SurgeriesController : ControllerBase
    {
        private readonly ISurgeryService _surgeriesService;

        public SurgeriesController(ISurgeryService surgeriesService)
        {
            _surgeriesService = surgeriesService;
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetSurgeries([FromQuery] DateTime? date, [FromQuery] string? term, [FromQuery] SurgeryStatusEnum? status, [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var surgeries = await _surgeriesService.GetPatientsWithSurgeriesAsync(date, term, status, page, size);
            return Ok(surgeries);
        }

        [HttpGet("{surgeryId}/{patientId}")]
        //[Authorize]
        public async Task<IActionResult> GetPatientBySurgeryId([FromRoute] int surgeryId, string patientId)
        {
            var surgerie = await _surgeriesService.GetPatientAnesthesiaRecordByIdAsync(patientId, surgeryId);
            return Ok(surgerie);
        }

        [HttpPatch("{patientId}/{surgeryId}/{responsableId}")]
        //[Authorize]
        public async Task<IActionResult> AssumePatient([FromRoute] string patientId, int surgeryId, int? responsableId)
        {
            var mappedList = await _surgeriesService.AssumePatientAsync(patientId, surgeryId, responsableId);
            return Ok(mappedList);
        }
    }
}