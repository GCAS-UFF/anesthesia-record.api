namespace UFF.FichaAnestesica.Domain.Entities
{
    public class InstitutionSettings : Base
    {
        public const int DefaultMonitoringIntervalMinutes = 5;
        public const string DefaultHospitalName = "Hospital Universitário Antônio Pedro";
        public const string DefaultHospitalSector = "Centro Cirúrgico — Anestesiologia";
        public const string DefaultHospitalCity = "Niterói";
        public const string DefaultHospitalState = "RJ";

        protected InstitutionSettings() { }

        public int MonitoringIntervalMinutes { get; protected set; }
        public string? SigaApiUrl { get; protected set; }
        public string? AghuApiUrl { get; protected set; }
        public string HospitalName { get; protected set; }
        public string? HospitalSector { get; protected set; }
        public string? HospitalCnpj { get; protected set; }
        public string? HospitalCep { get; protected set; }
        public string? HospitalStreet { get; protected set; }
        public string? HospitalNumber { get; protected set; }
        public string? HospitalNeighborhood { get; protected set; }
        public string HospitalCity { get; protected set; }
        public string HospitalState { get; protected set; }
        public int? UpdatedByUserId { get; protected set; }

        public static InstitutionSettings CreateDefault()
        {
            return new InstitutionSettings
            {
                MonitoringIntervalMinutes = DefaultMonitoringIntervalMinutes,
                HospitalName = DefaultHospitalName,
                HospitalSector = DefaultHospitalSector,
                HospitalCity = DefaultHospitalCity,
                HospitalState = DefaultHospitalState,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void Update(
            int monitoringIntervalMinutes,
            string? sigaApiUrl,
            string? aghuApiUrl,
            string hospitalName,
            string? hospitalSector,
            string? hospitalCnpj,
            string? hospitalCep,
            string? hospitalStreet,
            string? hospitalNumber,
            string? hospitalNeighborhood,
            string hospitalCity,
            string hospitalState,
            int updatedByUserId)
        {
            MonitoringIntervalMinutes = monitoringIntervalMinutes;
            SigaApiUrl = sigaApiUrl;
            AghuApiUrl = aghuApiUrl;
            HospitalName = string.IsNullOrWhiteSpace(hospitalName) ? HospitalName : hospitalName;
            HospitalSector = hospitalSector;
            HospitalCnpj = hospitalCnpj;
            HospitalCep = hospitalCep;
            HospitalStreet = hospitalStreet;
            HospitalNumber = hospitalNumber;
            HospitalNeighborhood = hospitalNeighborhood;
            HospitalCity = string.IsNullOrWhiteSpace(hospitalCity) ? HospitalCity : hospitalCity;
            HospitalState = string.IsNullOrWhiteSpace(hospitalState) ? HospitalState : hospitalState;
            UpdatedByUserId = updatedByUserId;
        }
    }
}
