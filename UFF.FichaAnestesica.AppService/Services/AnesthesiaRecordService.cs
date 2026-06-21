using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Response;
using UFF.FichaAnestesica.Domain.Services;

public class AnesthesiaRecordService : IAnesthesiaRecordService
{
    private readonly IAnesthesiaRecordRepository _anesthesiaRecordRepository;
    private readonly IMonitoringRecordRepository _monitoringRecordRepository;

    public AnesthesiaRecordService(IAnesthesiaRecordRepository anesthesiaRecordRepository, IMonitoringRecordRepository monitoringRecordRepository)
    {
        _anesthesiaRecordRepository = anesthesiaRecordRepository;
        _monitoringRecordRepository = monitoringRecordRepository;
    }

    public async Task<CommandResult> GetByIdAsync(int id)
    {
        var anestesiaRecord = await _anesthesiaRecordRepository.GetByIdAsync(id);

        if (anestesiaRecord == null)
            throw new Exception("Nenhuma ficha encontrada com esse identificador");

        return new CommandResult(true, AnesthesiaRecordResponse.ToResponse(anestesiaRecord));
    }

    public async Task<CommandResult> Create(AnesthesiaRecordCommand command)
    {
        var anesthesiaRecord = AnesthesiaRecord.Create(command);

        try
        {
            await _anesthesiaRecordRepository.AddAsync(anesthesiaRecord);
            await _anesthesiaRecordRepository.SaveChangesAsync();
            var monitoring = MonitoringRecord.Create(new MonitoringRecordCommand(anesthesiaRecord.Id));
            await _monitoringRecordRepository.AddAsync(monitoring);
            await _anesthesiaRecordRepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return new CommandResult(false, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
        }

        return new CommandResult(true, AnesthesiaRecordResponse.ToResponse(anesthesiaRecord));
    }

    public async Task<CommandResult> Update(int id, AnesthesiaRecordCommand command)
    {
        var anesthesiaRecord = await _anesthesiaRecordRepository.GetByIdAsync(id);

        if (anesthesiaRecord == null)
            throw new Exception("Ficha anest�sica n�o encontrada");

        //if (anesthesiaRecord.AnesthesiaRecordStatus == UFF.FichaAnestesica.Domain.Enums.AnesthesiaRecordStatus.Completed)
        //    throw new Exception("N�o � poss�vel alterar uma ficha salva previamente");

        try
        {
            anesthesiaRecord.Update(command);

            _anesthesiaRecordRepository.Update(anesthesiaRecord);
            await _anesthesiaRecordRepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return new CommandResult(false, ex.Message);
        }
      
        return CommandResult.Success(AnesthesiaRecordResponse.ToResponse(anesthesiaRecord));
    }   
}
