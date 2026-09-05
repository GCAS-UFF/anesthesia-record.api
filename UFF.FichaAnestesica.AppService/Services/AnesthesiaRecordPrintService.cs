using Microsoft.Extensions.Logging;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Domain.Response;
using UFF.FichaAnestesica.Domain.Response.Print;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Infra.Services
{
    public class AnesthesiaRecordPrintService : IAnesthesiaRecordPrintService
    {
        private readonly IAnesthesiaRecordRepository _anesthesiaRecordRepository;
        private readonly IPatientReadOnlyRepository _patientReadOnlyRepository;
        private readonly IPreAnesthesiaRecordRepository _preAnesthesiaRecordRepository;
        private readonly IMonitoringRecordRepository _monitoringRecordRepository;
        private readonly IInstitutionSettingsRepository _institutionSettingsRepository;
        private readonly ILogger<AnesthesiaRecordPrintService> _logger;

        public AnesthesiaRecordPrintService(
            IAnesthesiaRecordRepository anesthesiaRecordRepository,
            IPatientReadOnlyRepository patientReadOnlyRepository,
            IPreAnesthesiaRecordRepository preAnesthesiaRecordRepository,
            IMonitoringRecordRepository monitoringRecordRepository,
            IInstitutionSettingsRepository institutionSettingsRepository,
            ILogger<AnesthesiaRecordPrintService> logger)
        {
            _anesthesiaRecordRepository = anesthesiaRecordRepository;
            _patientReadOnlyRepository = patientReadOnlyRepository;
            _preAnesthesiaRecordRepository = preAnesthesiaRecordRepository;
            _monitoringRecordRepository = monitoringRecordRepository;
            _institutionSettingsRepository = institutionSettingsRepository;
            _logger = logger;
        }

        public async Task<AnesthesiaRecordPrintViewModel?> BuildAsync(int id)
        {
            _logger.LogInformation("[PDF] Iniciando montagem do relatório da ficha {Id}.", id);

            var anesthesiaRecord = await _anesthesiaRecordRepository.GetByIdAsync(id);

            if (anesthesiaRecord == null)
            {
                _logger.LogWarning("[PDF] Ficha {Id} não encontrada.", id);
                return null;
            }

            var patient = await _patientReadOnlyRepository.GetFromHospitalByPatientIdAndSurgeryIdAsync(anesthesiaRecord.PatientId, id)
                ?? new PatientDetailDto { PatientId = anesthesiaRecord.PatientId };

            var preAnesthesiaRecord = await _preAnesthesiaRecordRepository.GetByAnesthesiaRecordIdAsync(id);

            _logger.LogInformation("[PDF] Dados do paciente e avaliação pré-anestésica carregados. Carregando monitorização...");
            var monitoringRecord = await _monitoringRecordRepository.GetCompleteByIdAsync(id);
            _logger.LogInformation("[PDF] Monitorização carregada ({HasMonitoring}).", monitoringRecord != null ? "presente" : "ausente");

            var institution = await _institutionSettingsRepository.GetSingletonAsync();

            var monitoringResponse = monitoringRecord != null ? MonitoringRecordResponse.ToResponse(monitoringRecord) : null;

            _logger.LogInformation("[PDF] Montando gráfico de monitorização...");
            var chart = MonitoringChartBuilder.Build(monitoringResponse, _logger);
            _logger.LogInformation("[PDF] Gráfico de monitorização montado ({Rows} bloco(s)).", chart.Rows.Count);

            var viewModel = new AnesthesiaRecordPrintViewModel
            {
                Hospital = BuildHospitalInfo(institution),
                Record = AnesthesiaRecordResponse.ToResponse(anesthesiaRecord, patient),
                PreAnesthesia = preAnesthesiaRecord != null ? PreAnesthesiaRecordResponse.ToResponse(preAnesthesiaRecord) : null,
                Monitoring = monitoringResponse,
                FluidTotals = BuildFluidTotals(monitoringRecord),
                Chart = chart,
                PrintedAt = DateTime.Now
            };

            _logger.LogInformation("[PDF] Relatório da ficha {Id} montado com sucesso.", id);

            return viewModel;
        }

        private static PrintHospitalInfo BuildHospitalInfo(InstitutionSettings? institution)
        {
            if (institution == null)
            {
                return new PrintHospitalInfo
                {
                    Name = InstitutionSettings.DefaultHospitalName,
                    Sector = InstitutionSettings.DefaultHospitalSector
                };
            }

            var addressParts = new List<string>();

            if (!string.IsNullOrWhiteSpace(institution.HospitalStreet))
            {
                addressParts.Add(string.IsNullOrWhiteSpace(institution.HospitalNumber)
                    ? institution.HospitalStreet
                    : $"{institution.HospitalStreet}, {institution.HospitalNumber}");
            }

            if (!string.IsNullOrWhiteSpace(institution.HospitalNeighborhood))
                addressParts.Add(institution.HospitalNeighborhood);

            if (!string.IsNullOrWhiteSpace(institution.HospitalCity))
                addressParts.Add($"{institution.HospitalCity}/{institution.HospitalState}");

            return new PrintHospitalInfo
            {
                Name = institution.HospitalName,
                Sector = institution.HospitalSector,
                Cnpj = institution.HospitalCnpj,
                Address = addressParts.Count > 0 ? string.Join(" - ", addressParts) : null
            };
        }

        private static PrintFluidBalanceTotals BuildFluidTotals(MonitoringRecord? monitoringRecord)
        {
            var totals = new PrintFluidBalanceTotals();

            if (monitoringRecord == null)
                return totals;

            foreach (var fluid in monitoringRecord.FluidBalances)
            {
                if (fluid.Type == FluidBalanceTypeEnum.Gain)
                    totals.GainsMl += fluid.VolumeMl;
                else
                    totals.LossesMl += fluid.VolumeMl;
            }

            return totals;
        }
    }
}
