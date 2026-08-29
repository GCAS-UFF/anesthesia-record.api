using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Domain.Response;
using UFF.FichaAnestesica.Domain.Services;

public class AnesthesiaRecordService : IAnesthesiaRecordService
{
    private readonly IAnesthesiaRecordRepository _anesthesiaRecordRepository;
    private readonly IMonitoringRecordRepository _monitoringRecordRepository;
    private readonly IPatientReadOnlyRepository _hospitalApiRepository;
    private readonly IProcedureRepository _procedureRepository;
    private readonly ICurrentUserService _currentUserService;

    public AnesthesiaRecordService(IAnesthesiaRecordRepository anesthesiaRecordRepository, IMonitoringRecordRepository monitoringRecordRepository, IPatientReadOnlyRepository hospitalApiRepository,
        IProcedureRepository procedureRepository, ICurrentUserService currentUserService)
    {
        _anesthesiaRecordRepository = anesthesiaRecordRepository;
        _monitoringRecordRepository = monitoringRecordRepository;
        _hospitalApiRepository = hospitalApiRepository;
        _procedureRepository = procedureRepository;
        _currentUserService = currentUserService;
    }

    public async Task<CommandResult> GetByIdAsync(int id, string extenalPatientId)
    {
        var anestesiaRecord = await _anesthesiaRecordRepository.GetByIdAsync(id);

        if (anestesiaRecord == null)
            return await this.Create(new AnesthesiaRecordCommand()
            {
                SurgeryId = id,
                PatientId = extenalPatientId
            });

        var patient = await _hospitalApiRepository.GetFromHospitalByPatientIdAndSurgeryIdAsync(extenalPatientId, id);


        return new CommandResult(true, AnesthesiaRecordResponse.ToResponse(anestesiaRecord, patient));
    }

    public async Task<CommandResult> Create(AnesthesiaRecordCommand command)
    {
        var patient = await _hospitalApiRepository.GetFromHospitalByPatientIdAndSurgeryIdAsync(command.PatientId, command.SurgeryId);
        var anesthesiaRecord = AnesthesiaRecord.Create(command, patient.SurgeryDate);

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

        return new CommandResult(true, AnesthesiaRecordResponse.ToResponse(anesthesiaRecord, patient));
    }

    public async Task<CommandResult> Update(int id, AnesthesiaRecordCommand command)
    {
        var anesthesiaRecord = await _anesthesiaRecordRepository.GetByIdAsync(id);
        var patient = await _hospitalApiRepository.GetFromHospitalByPatientIdAndSurgeryIdAsync(command.PatientId, command.SurgeryId);

        if (anesthesiaRecord == null)
            throw new Exception("Ficha anestésica não encontrada");

        if (anesthesiaRecord.FirstAnesthesiologistId.HasValue && anesthesiaRecord.FirstAnesthesiologistId != _currentUserService.UserId)
            return CommandResult.Forbid("Apenas o médico responsável pode editar esta ficha.");

        if (anesthesiaRecord.Status == SurgeryStatusEnum.Completed)
            throw new Exception("Não é possível alterar uma ficha depois da cirurgia finalizada.");

        try
        {
            var procedureIds = command.Surgeries.Select(x => x.Id).ToList();

            var procedures = await _procedureRepository.GetByIdsAsync(procedureIds);

            anesthesiaRecord.Update(command);

            await _anesthesiaRecordRepository.RemoveProceduresAsync(id);

            anesthesiaRecord.AddProcedures(command.Surgeries, procedures);

            if (command.Finalize)
            {
                var monitoringRecord = await _monitoringRecordRepository.GetByAnesthesiaRecordIdAsync(id);

                if (monitoringRecord == null || monitoringRecord.Status != SurgeryStatusEnum.Completed)
                    return new CommandResult(false, "O monitoramento ainda não foi finalizado. Finalize a anestesia na tela de Monitoramento antes de concluir a ficha.");

                anesthesiaRecord.SetStatus(SurgeryStatusEnum.Completed);
            }

            _anesthesiaRecordRepository.Update(anesthesiaRecord);

            await _anesthesiaRecordRepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return new CommandResult(false, ex.Message);
        }

        return CommandResult.Success(AnesthesiaRecordResponse.ToResponse(anesthesiaRecord, patient));
    }

    public async Task<CommandResult> Reopen(int id)
    {
        var anesthesiaRecord = await _anesthesiaRecordRepository.GetByIdAsync(id);

        if (anesthesiaRecord == null)
            return CommandResult.Fail("Ficha anestésica não encontrada");

        if (anesthesiaRecord.Status != SurgeryStatusEnum.Completed)
            return CommandResult.Fail("Esta ficha não está finalizada.");

        try
        {
            anesthesiaRecord.SetStatus(SurgeryStatusEnum.InProgress);
            _anesthesiaRecordRepository.Update(anesthesiaRecord);
            await _anesthesiaRecordRepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return CommandResult.Fail(ex.Message);
        }

        return CommandResult.Success(new { anesthesiaRecord.Id, anesthesiaRecord.Status });
    }
}