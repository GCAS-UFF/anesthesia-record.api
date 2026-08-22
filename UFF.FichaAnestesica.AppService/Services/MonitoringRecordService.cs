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

    public MonitoringRecordService(IMonitoringRecordRepository repository, IAnesthesiaRecordRepository anesthesiaRecordRepository)
    {
        _monitoringRepository = repository;
        _anesthesiaRecordRepository = anesthesiaRecordRepository;
    }

    public async Task<CommandResult> GetByIdAsync(int id)
    {
        var monitoring = await _monitoringRepository.GetCompleteByIdAsync(id);

        if (monitoring == null)
            return new CommandResult(false, "Monitoriza��o n�o encontrada");

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

            // Persiste o snapshot final do monitoramento (vitais/agentes/eventos/balan�o/posi��es),
            // se enviado. N�o altera o status da FICHA anest�sica em nenhuma hip�tese: finalizar o
            // monitoramento e finalizar a ficha s�o momentos distintos.
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