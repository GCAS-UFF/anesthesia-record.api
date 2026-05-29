using Microsoft.AspNetCore.Mvc;
using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Infra.Repositories.Aghu;
using UFF.FichaAnestesica.Service.Services.Aghu;

[ApiController]
[Route("integrations")]
public class IntegrationController : ControllerBase
{
    private readonly IProfessionalApiService _professionalApiService;
    private readonly IMedicineApiService _medicineApiService;

    public IntegrationController(IProfessionalApiService professionalApiService, IMedicineApiService medicineApiService)
    {
        _professionalApiService = professionalApiService;
        _medicineApiService = medicineApiService;
    }

    [HttpPost("sync/professionals")]
    public async Task<IActionResult> SyncProfessionals()
    {
        await _professionalApiService.SyncProfessionals();
        return Ok(new CommandResult(true));     
    }

    [HttpPost("sync/medicines")]
    public async Task<IActionResult> SyncMedicines()
    {
        await _medicineApiService.SyncMedicines();
        return Ok(new CommandResult(true));
    }
}