using Microsoft.Extensions.Logging;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Domain.Response;
using UFF.FichaAnestesica.Domain.Response.Print;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Infra.Services
{
    public class PreAnesthesiaRecordPrintService : IPreAnesthesiaRecordPrintService
    {
        private readonly IAnesthesiaRecordRepository _anesthesiaRecordRepository;
        private readonly IPatientReadOnlyRepository _patientReadOnlyRepository;
        private readonly IPreAnesthesiaRecordRepository _preAnesthesiaRecordRepository;
        private readonly IInstitutionSettingsRepository _institutionSettingsRepository;
        private readonly ILogger<PreAnesthesiaRecordPrintService> _logger;

        public PreAnesthesiaRecordPrintService(
            IAnesthesiaRecordRepository anesthesiaRecordRepository,
            IPatientReadOnlyRepository patientReadOnlyRepository,
            IPreAnesthesiaRecordRepository preAnesthesiaRecordRepository,
            IInstitutionSettingsRepository institutionSettingsRepository,
            ILogger<PreAnesthesiaRecordPrintService> logger)
        {
            _anesthesiaRecordRepository = anesthesiaRecordRepository;
            _patientReadOnlyRepository = patientReadOnlyRepository;
            _preAnesthesiaRecordRepository = preAnesthesiaRecordRepository;
            _institutionSettingsRepository = institutionSettingsRepository;
            _logger = logger;
        }

        public async Task<PreAnesthesiaRecordPrintViewModel?> BuildAsync(int anesthesiaRecordId)
        {
            _logger.LogInformation("[PDF] Iniciando montagem do relatório da avaliação pré-anestésica {Id}.", anesthesiaRecordId);

            var preAnesthesiaRecord = await _preAnesthesiaRecordRepository.GetByAnesthesiaRecordIdAsync(anesthesiaRecordId);

            if (preAnesthesiaRecord == null)
            {
                _logger.LogWarning("[PDF] Avaliação pré-anestésica não encontrada para a ficha {Id}.", anesthesiaRecordId);
                return null;
            }

            var anesthesiaRecord = await _anesthesiaRecordRepository.GetByIdAsync(anesthesiaRecordId);

            if (anesthesiaRecord == null)
            {
                _logger.LogWarning("[PDF] Ficha {Id} não encontrada.", anesthesiaRecordId);
                return null;
            }

            var patient = await _patientReadOnlyRepository.GetFromHospitalByPatientIdAndSurgeryIdAsync(anesthesiaRecord.PatientId, anesthesiaRecordId)
                ?? new PatientDetailDto { PatientId = anesthesiaRecord.PatientId };

            var institution = await _institutionSettingsRepository.GetSingletonAsync();

            var viewModel = new PreAnesthesiaRecordPrintViewModel
            {
                Hospital = PrintViewModelHelpers.BuildHospitalInfo(institution),
                Record = AnesthesiaRecordResponse.ToResponse(anesthesiaRecord, patient),
                PreAnesthesia = PreAnesthesiaRecordResponse.ToResponse(preAnesthesiaRecord),
                PrintedAt = DateTime.Now
            };

            _logger.LogInformation("[PDF] Relatório da avaliação pré-anestésica {Id} montado com sucesso.", anesthesiaRecordId);

            return viewModel;
        }
    }
}
