using Microsoft.AspNetCore.Mvc;
using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Infra.Repositories.Aghu;
using UFF.FichaAnestesica.Service.Services.Aghu;

[ApiController]
[Route("api/[controller]")]
public class IntegrationsController : ControllerBase
{
    private readonly IProfessionalApiService _professionalApiService;
    private readonly IMedicineApiService _medicineApiService;

    public IntegrationsController(IProfessionalApiService professionalApiService, IMedicineApiService medicineApiService)
    {
        _professionalApiService = professionalApiService;
        _medicineApiService = medicineApiService;
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
}