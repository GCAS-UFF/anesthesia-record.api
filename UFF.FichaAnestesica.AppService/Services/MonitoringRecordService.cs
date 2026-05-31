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
            return new CommandResult(false, "Monitorização não encontrada");

        return new CommandResult(true, MonitoringRecordResponse.ToResponse(monitoring));
    }

    public async Task<CommandResult> Create(MonitoringRecordCommand command)
    {
        try
        {
            var monitoring = MonitoringRecord.Create(command);

            await _monitoringRepository.AddAsync(monitoring);
            await _monitoringRepository.SaveChangesAsync();

            return new CommandResult(true, MonitoringRecordResponse.ToResponse(monitoring));
        }
        catch (Exception ex)
        {
            return new CommandResult(false, ex.Message);
        }
    }

    public async Task<CommandResult> Update(int id, MonitoringRecordCommand command)
    {
        var monitoring = await _monitoringRepository.GetCompleteByIdAsync(id);


        if (monitoring == null)
            return new CommandResult(false, "Monitorização não encontrada");

        try
        {
            monitoring.Update(command);
            _monitoringRepository.Update(monitoring);

            await _monitoringRepository.SaveChangesAsync();

            return new CommandResult(true, MonitoringRecordResponse.ToResponse(monitoring));
        }
        catch (Exception ex)
        {
            return new CommandResult(false, ex.Message);
        }
    }

    public async Task<CommandResult> FinalizePatientAsync(int anesthesiaRecordId)
    {
        try
        {
            var monitoringRecord = await _monitoringRepository.GetByIdAsync(anesthesiaRecordId);

            if (monitoringRecord == null)
                throw new Exception("Ficha anestésica não encontrada");

            monitoringRecord.SetStatus(SurgeryStatusEnum.Completed);

            if (monitoringRecord.AnesthesiaRecord != null) { }
                monitoringRecord.AnesthesiaRecord.SetStatus(SurgeryStatusEnum.Completed);

            _monitoringRepository.Update(monitoringRecord);

            await _anesthesiaRecordRepository.SaveChangesAsync();

            return CommandResult.Success("Finalizado");

        }
        catch (Exception ex)
        {
            return CommandResult.Fail(ex.Message);
        }
    }
}