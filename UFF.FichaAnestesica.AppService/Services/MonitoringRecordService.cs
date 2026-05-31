using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Response;
using UFF.FichaAnestesica.Domain.Services;

public class MonitoringRecordService : IMonitoringRecordService
{
    private readonly IMonitoringRecordRepository _repository;
    private readonly IAnesthesiaRecordRepository _anesthesiaRecordRepository;

    public MonitoringRecordService(IMonitoringRecordRepository repository, IAnesthesiaRecordRepository anesthesiaRecordRepository)
    {
        _repository = repository;
        _anesthesiaRecordRepository = anesthesiaRecordRepository;
    }

    public async Task<CommandResult> GetByIdAsync(int id)
    {
        var monitoring = await _repository.GetCompleteByIdAsync(id);

        if (monitoring == null)
            return new CommandResult(false, "Monitorização não encontrada");

        return new CommandResult(true, MonitoringRecordResponse.ToResponse(monitoring));
    }

    public async Task<CommandResult> Create(MonitoringRecordCommand command)
    {
        try
        {
            var monitoring = MonitoringRecord.Create(command);

            await _repository.AddAsync(monitoring);
            await _repository.SaveChangesAsync();

            return new CommandResult(true, MonitoringRecordResponse.ToResponse(monitoring));
        }
        catch (Exception ex)
        {
            return new CommandResult(false, ex.Message);
        }
    }

    public async Task<CommandResult> Update(int id, MonitoringRecordCommand command)
    {
        var monitoring = await _repository.GetCompleteByIdAsync(id);


        if (monitoring == null)
            return new CommandResult(false, "Monitorização não encontrada");

        try
        {
            monitoring.Update(command);
            _repository.Update(monitoring);

            await _repository.SaveChangesAsync();

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

            var anesthesiaRecord = await _anesthesiaRecordRepository.GetByIdAsync(anesthesiaRecordId);

            if (anesthesiaRecord == null)
                throw new Exception("Ficha anestésica não encontrada");

            anesthesiaRecord.SetStatus(AnesthesiaRecordStatus.Completed);

            if (anesthesiaRecord.MonitoringRecord != null)
                anesthesiaRecord.MonitoringRecord.SetStatus(SurgeryStatusEnum.Completed);

            await _anesthesiaRecordRepository.SaveChangesAsync();

            return CommandResult.Success("Finalizado");

        }
        catch (Exception ex)
        {
            return CommandResult.Fail(ex.Message);
        }
    }
}