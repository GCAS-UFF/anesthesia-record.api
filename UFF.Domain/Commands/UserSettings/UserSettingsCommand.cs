namespace UFF.FichaAnestesica.Domain.Commands.UserSettings
{
    public class UserSettingsCommand
    {
        public string Language { get; set; }
        public int MonitoringIntervalMinutes { get; set; }
        public bool UseInstitutionalInterval { get; set; }
    }
}
