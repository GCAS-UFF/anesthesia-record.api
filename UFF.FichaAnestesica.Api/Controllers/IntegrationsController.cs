using Microsoft.AspNetCore.Mvc;
using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Services;
using UFF.FichaAnestesica.Infra.Repositories.Aghu;
using UFF.FichaAnestesica.Service.Services.Aghu;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class IntegrationsController : ControllerBase
{
    private readonly IProfessionalApiService _professionalApiService;
    private readonly IMedicineApiService _medicineApiService;
    private readonly IProcedureApiService _procedureApiService;
    private readonly IProcedureService _procedureService;
    private readonly IDrugService _drugService;
    private readonly IProfessionalService _professionalService;

    public IntegrationsController(IProfessionalApiService professionalApiService, IMedicineApiService medicineApiService, IProcedureApiService procedureApiService, 
        IProcedureService procedureService, IDrugService drugService, IProfessionalService professionalService)
    {
        _professionalApiService = professionalApiService;
        _medicineApiService = medicineApiService;
        _procedureApiService = procedureApiService;
        _procedureService = procedureService;
        _drugService = drugService;
        _professionalService = professionalService;
    }

    [HttpPost("sync/professionals")]
    public async Task<IActionResult> SyncProfessionals()
    {
        try
        {
            var total = await _professionalApiService.SyncProfessionals();
            return Ok(CommandResult.Success(total));
        }
        catch (Exception ex)
        {
            return BadRequest(CommandResult.Fail(ex.Message));
        }
    }

    [HttpPost("sync/medicines")]
    public async Task<IActionResult> SyncMedicines()
    {
        try
        {
            var total = await _medicineApiService.SyncMedicines();
            return Ok(CommandResult.Success(total));
        }
        catch (Exception ex)
        {
            return BadRequest(CommandResult.Fail(ex.Message));
        }
    }

    [HttpPost("sync/procedures")]
    public async Task<IActionResult> SyncProcedures()
    {
        try
        {
            var total = await _procedureApiService.SyncProcedures();
            return Ok(CommandResult.Success(total));
        }
        catch (Exception ex)
        {
            return BadRequest(CommandResult.Fail(ex.Message));
        }
    }

    [HttpGet("sync/last-integrations")]
    public async Task<IActionResult> GetLastIntegrations()
    {
        try
        {
            var procedureLastIntegration = await _procedureService.GetLasIntegrationTime();
            var drugLastIntegration = await _drugService.GetLasIntegrationTime();
            var professioalIntegration = await _professionalService.GetLasIntegrationTime();

            var lastIntegrations = new
            {
                Procedures = procedureLastIntegration,
                Medicines = drugLastIntegration, 
                Professionals = professioalIntegration
            };

            return Ok(CommandResult.Success(lastIntegrations));
        }
        catch (Exception ex)
        {
            return BadRequest(CommandResult.Fail(ex.Message));
        }
    }
}