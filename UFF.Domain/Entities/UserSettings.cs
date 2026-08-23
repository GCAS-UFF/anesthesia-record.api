namespace UFF.FichaAnestesica.Domain.Entities
{
    public class UserSettings : Base
    {
        public const string DefaultLanguage = "pt-BR";
        public const int DefaultMonitoringIntervalMinutes = 5;

        protected UserSettings() { }

        public int UserId { get; protected set; }
        public User User { get; protected set; }
        public string Language { get; protected set; }
        public int MonitoringIntervalMinutes { get; protected set; }
        public bool UseInstitutionalInterval { get; protected set; }

        public static UserSettings CreateDefault(int userId)
        {
            return new UserSettings
            {
                UserId = userId,
                Language = DefaultLanguage,
                MonitoringIntervalMinutes = DefaultMonitoringIntervalMinutes,
                UseInstitutionalInterval = true,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void Update(string language, int monitoringIntervalMinutes, bool useInstitutionalInterval)
        {
            Language = string.IsNullOrWhiteSpace(language) ? Language : language;
            MonitoringIntervalMinutes = monitoringIntervalMinutes;
            UseInstitutionalInterval = useInstitutionalInterval;
        }
    }
}
