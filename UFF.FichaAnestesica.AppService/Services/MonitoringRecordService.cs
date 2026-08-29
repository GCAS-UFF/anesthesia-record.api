using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Response;
using UFF.FichaAnestesica.Domain.Services;

public class MonitoringRecordService : IMonitoringRecordService
{
    private readonly IMonitoringRecordRepository _monitoringRepository;
    private readonly IAnesthesiaRecordRepository _anesthesiaRecordRepository;
    private readonly ICurrentUserService _currentUserService;

    public MonitoringRecordService(IMonitoringRecordRepository repository, IAnesthesiaRecordRepository anesthesiaRecordRepository, ICurrentUserService currentUserService)
    {
        _monitoringRepository = repository;
        _anesthesiaRecordRepository = anesthesiaRecordRepository;
        _currentUserService = currentUserService;
    }

    private bool IsResponsibleDoctor(MonitoringRecord monitoring)
    {
        var firstAnesthesiologistId = monitoring.AnesthesiaRecord?.FirstAnesthesiologistId;
        return firstAnesthesiologistId.HasValue && firstAnesthesiologistId == _currentUserService.UserId;
    }

    public async Task<CommandResult> GetByIdAsync(int id)
    {
        var monitoring = await _monitoringRepository.GetCompleteByIdAsync(id);

        if (monitoring == null)
            return new CommandResult(false, "Monitorização não encontrada");

        if (!IsResponsibleDoctor(monitoring) && monitoring.Status != SurgeryStatusEnum.Completed)
            return CommandResult.Forbid("Monitorização ainda não concluída.");

        return CommandResult.Success(MonitoringRecordResponse.ToResponse(monitoring));
    }

    public async Task<CommandResult> Create(MonitoringRecordCommand command)
    {
        try
        {
            var monitoring = MonitoringRecord.Create(command);

            await _monitoringRepository.AddAsync(monitoring);
            await _monitoringRepository.SaveChangesAsync();

            return CommandResult.Success(MonitoringRecordResponse.ToResponse(monitoring));
        }
        catch (Exception ex)
        {
            return CommandResult.Fail(ex.Message);
        }
    }

    public async Task<CommandResult> Update(int id, MonitoringRecordCommand command)
    {
        var monitoring = await _monitoringRepository.GetCompleteByIdAsync(id);

        if (monitoring == null)
            return CommandResult.Fail("Monitoriza��o n�o encontrada");

        if (!IsResponsibleDoctor(monitoring))
            return CommandResult.Forbid("Apenas o médico responsável pode editar a monitorização.");

        if (monitoring.Status == SurgeryStatusEnum.Completed)
            return CommandResult.Fail("Não é possível alterar uma monitorização depois de finalizada.");

        try
        {
            monitoring.Update(command);
            _monitoringRepository.Update(monitoring);

            await _monitoringRepository.SaveChangesAsync();

            return CommandResult.Success(MonitoringRecordResponse.ToResponse(monitoring));
        }
        catch (Exception ex)
        {
            return CommandResult.Fail(ex.Message);
        }
    }

    public async Task<CommandResult> FinalizePatientAsync(int anesthesiaRecordId, MonitoringRecordCommand? command)
    {
        try
        {
            var monitoringRecord = await _monitoringRepository.GetCompleteByIdAsync(anesthesiaRecordId);

            if (monitoringRecord == null)
                return CommandResult.Fail("Registro de monitoramento n�o encontrado");

            if (!IsResponsibleDoctor(monitoringRecord))
                return CommandResult.Forbid("Apenas o médico responsável pode finalizar a monitorização.");

            if (monitoringRecord.Status == SurgeryStatusEnum.Completed)
                return CommandResult.Success(MonitoringRecordResponse.ToResponse(monitoringRecord));

            if (command != null)
                monitoringRecord.Update(command);

            monitoringRecord.SetStatus(SurgeryStatusEnum.Completed);

            _monitoringRepository.Update(monitoringRecord);

            await _monitoringRepository.SaveChangesAsync();

            return CommandResult.Success(MonitoringRecordResponse.ToResponse(monitoringRecord));

        }
        catch (Exception ex)
        {
            return CommandResult.Fail(ex.Message);
        }
    }
}