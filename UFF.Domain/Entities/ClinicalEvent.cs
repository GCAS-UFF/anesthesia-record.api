namespace UFF.FichaAnestesica.Domain.Entities
{
    public class ClinicalEvent
    {
        public DateTime Timestamp { get; set; }
        public string EventType { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Observations { get; set; }
    }
}