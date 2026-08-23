namespace UFF.FichaAnestesica.Domain.Commands.UserSettings
{
    public class InstitutionSettingsCommand
    {
        public int MonitoringIntervalMinutes { get; set; }
        public string? SigaApiUrl { get; set; }
        public string? AghuApiUrl { get; set; }
        public string HospitalName { get; set; }
        public string? HospitalSector { get; set; }
        public string? HospitalCnpj { get; set; }
        public string? HospitalCep { get; set; }
        public string? HospitalStreet { get; set; }
        public string? HospitalNumber { get; set; }
        public string? HospitalNeighborhood { get; set; }
        public string HospitalCity { get; set; }
        public string HospitalState { get; set; }
    }
}
