using Microsoft.Extensions.Logging;
using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.PreAnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Extensions;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.Aghu;
using UFF.FichaAnestesica.Domain.Response;
using UFF.FichaAnestesica.Domain.Services;


public class PreAnesthesiaLabExamService : IPreAnesthesiaLabExamService
{
    public const string SkippedFinalized = "FINALIZED";
    public const string SkippedExistingRecordValues = "EXISTING_RECORD_VALUES";

    private const decimal MaxValue = 9_999_999_999m;
    private const int MaxSaveAttempts = 2;

    private readonly IPreAnesthesiaLabExamRepository _labExamRepository;
    private readonly IPreAnesthesiaRecordRepository _preAnesthesiaRecordRepository;
    private readonly IAnesthesiaRecordRepository _anesthesiaRecordRepository;
    private readonly ILabExamReadOnlyRepository _labExamReadOnlyRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<PreAnesthesiaLabExamService> _logger;

    public PreAnesthesiaLabExamService(
        IPreAnesthesiaLabExamRepository labExamRepository,
        IPreAnesthesiaRecordRepository preAnesthesiaRecordRepository,
        IAnesthesiaRecordRepository anesthesiaRecordRepository,
        ILabExamReadOnlyRepository labExamReadOnlyRepository,
        ICurrentUserService currentUserService,
        ILogger<PreAnesthesiaLabExamService> logger)
    {
        _labExamRepository = labExamRepository;
        _preAnesthesiaRecordRepository = preAnesthesiaRecordRepository;
        _anesthesiaRecordRepository = anesthesiaRecordRepository;
        _labExamReadOnlyRepository = labExamReadOnlyRepository;
        _currentUserService = currentUserService;
        _logger = logger;
    }

    private bool IsResponsibleDoctor(int? firstAnesthesiologistId)
        => firstAnesthesiologistId.HasValue && firstAnesthesiologistId == _currentUserService.UserId;

    public async Task<CommandResult> GetOrImportAsync(int anesthesiaRecordId)
    {
        if (anesthesiaRecordId <= 0)
            return CommandResult.Fail("Ficha anestésica inválida");

        var anesthesiaRecord = await _anesthesiaRecordRepository.GetByIdAsync(anesthesiaRecordId);
        if (anesthesiaRecord == null)
            return CommandResult.Fail("Cirurgia/ficha anestésica não encontrada");

        var labExam = await _labExamRepository.GetByAnesthesiaRecordIdAsync(anesthesiaRecordId);

        
        if (labExam != null && labExam.HasSigaData)
            return CommandResult.Success(PreAnesthesiaLabExamResponse.ToResponse(labExam));

        var preAnesthesiaRecord = await _preAnesthesiaRecordRepository.GetByAnesthesiaRecordIdAsync(anesthesiaRecordId);

        if (preAnesthesiaRecord?.IsFinalized == true)
            return CommandResult.Success(EmptyOrCurrent(labExam, anesthesiaRecordId,
                "A avaliação pré-anestésica está finalizada; os exames não são importados automaticamente.", SkippedFinalized));

        if (preAnesthesiaRecord?.HasImportableLabValues == true)
            return CommandResult.Success(EmptyOrCurrent(labExam, anesthesiaRecordId,
                "A avaliação já possui exames laboratoriais registrados; a importação do AGHU não foi realizada.", SkippedExistingRecordValues));

        if (string.IsNullOrWhiteSpace(anesthesiaRecord.PatientId))
            return await RegisterAttemptAsync(labExam, anesthesiaRecordId, LabExamImportStatusEnum.FAILED,
                "A ficha não possui paciente vinculado para consulta no AGHU.");

        AghuLabExamLookup lookup;
        try
        {
            
            lookup = await _labExamReadOnlyRepository.GetLatestReleasedByPatientIdAsync(anesthesiaRecord.PatientId);
        }
        catch (Exception ex) when (ex is HttpRequestException or OperationCanceledException or InvalidOperationException)
        {
            _logger.LogWarning("Falha ao importar exames laboratoriais do AGHU para a ficha {AnesthesiaRecordId}: {ErrorType}",
                anesthesiaRecordId, ex.GetType().Name);

            return await RegisterAttemptAsync(labExam, anesthesiaRecordId, LabExamImportStatusEnum.FAILED,
                "Não foi possível consultar os exames laboratoriais no AGHU. Tente novamente em instantes.");
        }

        if (!lookup.PatientFound)
            return await RegisterAttemptAsync(labExam, anesthesiaRecordId, LabExamImportStatusEnum.FAILED,
                "Paciente não localizado no AGHU.");

        var recognizedResults = lookup.Results
            .Where(r => LabAnalyteCatalog.FindByIntegratorKey(r.Analyte) != null)
            .ToList();

        if (lookup.Exam == null || recognizedResults.Count == 0)
            return await RegisterAttemptAsync(labExam, anesthesiaRecordId, LabExamImportStatusEnum.NO_EXAMS_AVAILABLE,
                lookup.Exam == null
                    ? "Nenhum exame laboratorial liberado foi encontrado no AGHU."
                    : "A coleta liberada mais recente no AGHU não possui resultados.");

        var isNew = labExam == null;
        labExam ??= PreAnesthesiaLabExam.Create(anesthesiaRecordId);
        labExam.RegisterImport(lookup.Exam, recognizedResults, DateTime.UtcNow);

        if (isNew)
            await _labExamRepository.AddAsync(labExam);

        if (!await _labExamRepository.TrySaveChangesAsync())
        {
            
            _logger.LogInformation("Importação concorrente de exames laboratoriais detectada para a ficha {AnesthesiaRecordId}", anesthesiaRecordId);
            return await CurrentStateAsync(anesthesiaRecordId);
        }

        _logger.LogInformation("Exames laboratoriais importados do AGHU para a ficha {AnesthesiaRecordId} (coleta {SourceExamId}, {Count} analitos)",
            anesthesiaRecordId, labExam.SourceExamId, labExam.Results.Count);

        return CommandResult.Success(PreAnesthesiaLabExamResponse.ToResponse(labExam, importedNow: true));
    }

    public async Task<CommandResult> SaveAsync(int anesthesiaRecordId, PreAnesthesiaLabExamCommand command)
    {
        if (anesthesiaRecordId <= 0)
            return CommandResult.Fail("Ficha anestésica inválida");

        if (command?.Results == null || command.Results.Count == 0)
            return CommandResult.Fail("Informe ao menos um resultado laboratorial");

        var values = new Dictionary<LabAnalyteEnum, decimal?>();
        foreach (var item in command.Results)
        {
            if (!LabAnalyteCatalog.TryParseAnalyte(item?.Analyte, out var analyte))
                return CommandResult.Fail($"Analito laboratorial inválido: {item?.Analyte}");

            if (!values.TryAdd(analyte, item!.Value))
                return CommandResult.Fail($"Analito laboratorial informado mais de uma vez: {analyte}");

            if (item.Value is < 0 or > MaxValue)
                return CommandResult.Fail($"Valor inválido para {analyte}");
        }

        var anesthesiaRecord = await _anesthesiaRecordRepository.GetByIdAsync(anesthesiaRecordId);
        if (anesthesiaRecord == null)
            return CommandResult.Fail("Cirurgia/ficha anestésica não encontrada");

        if (!IsResponsibleDoctor(anesthesiaRecord.FirstAnesthesiologistId))
            return CommandResult.Forbid("Apenas o médico responsável pode editar os exames da avaliação pré-anestésica.");

        var preAnesthesiaRecord = await _preAnesthesiaRecordRepository.GetByAnesthesiaRecordIdAsync(anesthesiaRecordId);
        if (preAnesthesiaRecord?.IsFinalized == true)
            return CommandResult.Fail("Esta avaliação pré-anestésica já foi finalizada. Solicite a um administrador que a libere para edição.");

        for (var attempt = 1; attempt <= MaxSaveAttempts; attempt++)
        {
            var labExam = await _labExamRepository.GetByAnesthesiaRecordIdAsync(anesthesiaRecordId);
            var isNew = labExam == null;
            labExam ??= PreAnesthesiaLabExam.Create(anesthesiaRecordId);

            if (!labExam.ApplyManualValues(values, DateTime.UtcNow))
                return CommandResult.Success(PreAnesthesiaLabExamResponse.ToResponse(labExam));

            if (isNew)
                await _labExamRepository.AddAsync(labExam);

            if (await _labExamRepository.TrySaveChangesAsync())
                return CommandResult.Success(PreAnesthesiaLabExamResponse.ToResponse(labExam));

          
            _logger.LogInformation("Conflito ao salvar exames laboratoriais da ficha {AnesthesiaRecordId} (tentativa {Attempt})",
                anesthesiaRecordId, attempt);
        }

        return CommandResult.Fail("Os exames laboratoriais foram alterados simultaneamente. Recarregue a ficha e tente novamente.");
    }

    private async Task<CommandResult> RegisterAttemptAsync(PreAnesthesiaLabExam? labExam, int anesthesiaRecordId, LabExamImportStatusEnum status, string message)
    {
        var isNew = labExam == null;
        labExam ??= PreAnesthesiaLabExam.Create(anesthesiaRecordId);
        labExam.RegisterImportAttempt(status, message, DateTime.UtcNow);

        if (isNew)
            await _labExamRepository.AddAsync(labExam);

        if (!await _labExamRepository.TrySaveChangesAsync())
            return await CurrentStateAsync(anesthesiaRecordId);

        return CommandResult.Success(PreAnesthesiaLabExamResponse.ToResponse(labExam));
    }

    private async Task<CommandResult> CurrentStateAsync(int anesthesiaRecordId)
    {
        var current = await _labExamRepository.GetByAnesthesiaRecordIdAsync(anesthesiaRecordId);

        return CommandResult.Success(current != null
            ? PreAnesthesiaLabExamResponse.ToResponse(current)
            : PreAnesthesiaLabExamResponse.Empty(anesthesiaRecordId));
    }

    private static PreAnesthesiaLabExamResponse EmptyOrCurrent(PreAnesthesiaLabExam? labExam, int anesthesiaRecordId, string message, string skippedReason)
    {
        var response = labExam != null
            ? PreAnesthesiaLabExamResponse.ToResponse(labExam)
            : PreAnesthesiaLabExamResponse.Empty(anesthesiaRecordId);

        response.Message = message;
        response.SkippedReason = skippedReason;
        return response;
    }
}
