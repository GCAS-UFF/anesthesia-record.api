using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Response;
using UFF.FichaAnestesica.Domain.Services;

public class AnesthesiaRecordService : IAnesthesiaRecordService
{
    private readonly IAnesthesiaRecordRepository _anesthesiaRecordRepository;

    public AnesthesiaRecordService(IAnesthesiaRecordRepository anesthesiaRecordRepository)
    {
        _anesthesiaRecordRepository = anesthesiaRecordRepository;
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
        }
        catch (Exception ex)
        {
            return new CommandResult(false, ex.Message);
        }      

        return new CommandResult(true, AnesthesiaRecordResponse.ToResponse(anesthesiaRecord));
    }

    public async Task<CommandResult> Update(int id, AnesthesiaRecordCommand command)
    {
        var anesthesiaRecord = await _anesthesiaRecordRepository.GetByIdAsync(id);

        if (anesthesiaRecord == null)
            throw new Exception("Ficha anestésica não encontrada");

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
      
        return new CommandResult(true, AnesthesiaRecordResponse.ToResponse(anesthesiaRecord));
    }   
}