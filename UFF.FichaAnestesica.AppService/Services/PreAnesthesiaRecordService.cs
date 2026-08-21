using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.PreAnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Response;
using UFF.FichaAnestesica.Domain.Services;

public class PreAnesthesiaRecordService : IPreAnesthesiaRecordService
{
    private readonly IPreAnesthesiaRecordRepository _preAnesthesiaRecordRepository;
    private readonly IAnesthesiaRecordRepository _anesthesiaRecordRepository;

    public PreAnesthesiaRecordService(IPreAnesthesiaRecordRepository preAnesthesiaRecordRepository, IAnesthesiaRecordRepository anesthesiaRecordRepository)
    {
        _preAnesthesiaRecordRepository = preAnesthesiaRecordRepository;
        _anesthesiaRecordRepository = anesthesiaRecordRepository;
    }

    public async Task<CommandResult> GetByIdAsync(int id)
    {
        var record = await _preAnesthesiaRecordRepository.GetCompleteByIdAsync(id);

        if (record == null)
            return new CommandResult(false, "Avaliação pré-anestésica não encontrada");

        return CommandResult.Success(PreAnesthesiaRecordResponse.ToResponse(record));
    }

    public async Task<CommandResult> GetByAnesthesiaRecordIdAsync(int anesthesiaRecordId)
    {
        var record = await _preAnesthesiaRecordRepository.GetByAnesthesiaRecordIdAsync(anesthesiaRecordId);

        if (record == null)
            return new CommandResult(false, "Avaliação pré-anestésica não encontrada");

        return CommandResult.Success(PreAnesthesiaRecordResponse.ToResponse(record));
    }

    public async Task<CommandResult> Create(PreAnesthesiaRecordCommand command)
    {        
        if (command.AsaClassification == null)
            return CommandResult.Fail("Classificação ASA é obrigatória");

        var anesthesiaRecord = await _anesthesiaRecordRepository.GetByIdAsync(command.AnesthesiaRecordId);
        if (anesthesiaRecord == null)
            return CommandResult.Fail("Cirurgia/ficha anestésica não encontrada");

        var existing = await _preAnesthesiaRecordRepository.GetByAnesthesiaRecordIdAsync(command.AnesthesiaRecordId);
        if (existing != null)
            return CommandResult.Fail("Já existe uma avaliação pré-anestésica para esta cirurgia. Use a atualização (PUT).");

        try
        {
            var record = PreAnesthesiaRecord.Create(command);

            await _preAnesthesiaRecordRepository.AddAsync(record);
            await _preAnesthesiaRecordRepository.SaveChangesAsync();

            var complete = await _preAnesthesiaRecordRepository.GetCompleteByIdAsync(record.Id);

            return CommandResult.Success(PreAnesthesiaRecordResponse.ToResponse(complete!));
        }
        catch (Exception ex)
        {
            return CommandResult.Fail(ex.Message);
        }
    }

    public async Task<CommandResult> Update(int id, PreAnesthesiaRecordCommand command)
    {
        if (command.AsaClassification == null)
            return CommandResult.Fail("Classificação ASA é obrigatória");

        var record = await _preAnesthesiaRecordRepository.GetCompleteByIdAsync(id);
        if (record == null)
            return CommandResult.Fail("Avaliação pré-anestésica não encontrada");

        try
        {
            record.Update(command);
            _preAnesthesiaRecordRepository.Update(record);

            await _preAnesthesiaRecordRepository.SaveChangesAsync();

            return CommandResult.Success(PreAnesthesiaRecordResponse.ToResponse(record));
        }
        catch (Exception ex)
        {
            return CommandResult.Fail(ex.Message);
        }
    }
}
