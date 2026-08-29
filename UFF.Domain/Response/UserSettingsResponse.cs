using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Domain.Response
{
    public class UserSettingsResponse
    {
        public bool IsAdmin { get; set; }
        public string Language { get; set; }
        public int MonitoringIntervalMinutes { get; set; }
        public bool UseInstitutionalInterval { get; set; }
        public int InstitutionalMonitoringIntervalMinutes { get; set; }
        public string HospitalName { get; set; }
        public InstitutionSettingsResponse Institution { get; set; }

        public static UserSettingsResponse ToResponse(
            UserSettings userSettings,
            InstitutionSettings institutionSettings,
            bool isAdmin)
        {
            return new UserSettingsResponse
            {
                IsAdmin = isAdmin,
                Language = userSettings.Language,
                MonitoringIntervalMinutes = userSettings.MonitoringIntervalMinutes,
                UseInstitutionalInterval = userSettings.UseInstitutionalInterval,
                InstitutionalMonitoringIntervalMinutes = institutionSettings.MonitoringIntervalMinutes,
                HospitalName = institutionSettings.HospitalName,
                Institution = InstitutionSettingsResponse.ToResponse(institutionSettings)
            };
        }
    }

    public class InstitutionSettingsResponse
    {
        public int MonitoringIntervalMinutes { get; set; }
        public string SigaApiUrl { get; set; }
        public string AghuApiUrl { get; set; }
        public string HospitalName { get; set; }
        public string HospitalSector { get; set; }
        public string HospitalCnpj { get; set; }
        public string HospitalCep { get; set; }
        public string HospitalStreet { get; set; }
        public string HospitalNumber { get; set; }
        public string HospitalNeighborhood { get; set; }
        public string HospitalCity { get; set; }
        public string HospitalState { get; set; }

        public static InstitutionSettingsResponse ToResponse(InstitutionSettings entity)
        {
            return new InstitutionSettingsResponse
            {
                MonitoringIntervalMinutes = entity.MonitoringIntervalMinutes,
                SigaApiUrl = entity.SigaApiUrl,
                AghuApiUrl = entity.AghuApiUrl,
                HospitalName = entity.HospitalName,
                HospitalSector = entity.HospitalSector,
                HospitalCnpj = entity.HospitalCnpj,
                HospitalCep = entity.HospitalCep,
                HospitalStreet = entity.HospitalStreet,
                HospitalNumber = entity.HospitalNumber,
                HospitalNeighborhood = entity.HospitalNeighborhood,
                HospitalCity = entity.HospitalCity,
                HospitalState = entity.HospitalState
            };
        }
    }
}
