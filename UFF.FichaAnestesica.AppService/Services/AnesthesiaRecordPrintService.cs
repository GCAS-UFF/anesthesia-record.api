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

        public AnesthesiaRecordPrintService(
            IAnesthesiaRecordRepository anesthesiaRecordRepository,
            IPatientReadOnlyRepository patientReadOnlyRepository,
            IPreAnesthesiaRecordRepository preAnesthesiaRecordRepository,
            IMonitoringRecordRepository monitoringRecordRepository,
            IInstitutionSettingsRepository institutionSettingsRepository)
        {
            _anesthesiaRecordRepository = anesthesiaRecordRepository;
            _patientReadOnlyRepository = patientReadOnlyRepository;
            _preAnesthesiaRecordRepository = preAnesthesiaRecordRepository;
            _monitoringRecordRepository = monitoringRecordRepository;
            _institutionSettingsRepository = institutionSettingsRepository;
        }

        public async Task<AnesthesiaRecordPrintViewModel?> BuildAsync(int id)
        {
            var anesthesiaRecord = await _anesthesiaRecordRepository.GetByIdAsync(id);

            if (anesthesiaRecord == null)
                return null;

            var patient = await _patientReadOnlyRepository.GetFromHospitalByPatientIdAndSurgeryIdAsync(anesthesiaRecord.PatientId, id)
                ?? new PatientDetailDto { PatientId = anesthesiaRecord.PatientId };

            var preAnesthesiaRecord = await _preAnesthesiaRecordRepository.GetByAnesthesiaRecordIdAsync(id);
            var monitoringRecord = await _monitoringRecordRepository.GetCompleteByIdAsync(id);
            var institution = await _institutionSettingsRepository.GetSingletonAsync();

            return new AnesthesiaRecordPrintViewModel
            {
                Hospital = BuildHospitalInfo(institution),
                Record = AnesthesiaRecordResponse.ToResponse(anesthesiaRecord, patient),
                PreAnesthesia = preAnesthesiaRecord != null ? PreAnesthesiaRecordResponse.ToResponse(preAnesthesiaRecord) : null,
                Monitoring = monitoringRecord != null ? MonitoringRecordResponse.ToResponse(monitoringRecord) : null,
                FluidTotals = BuildFluidTotals(monitoringRecord),
                PrintedAt = DateTime.Now
            };
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
