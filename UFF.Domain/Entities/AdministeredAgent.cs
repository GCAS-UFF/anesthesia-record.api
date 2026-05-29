namespace UFF.FichaAnestesica.Domain.Entities
{
    public class AdministeredAgent
    {
        public DateTime Timestamp { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Dose { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public string Route { get; set; } = string.Empty;
        public string Presentation { get; set; } = string.Empty;
    }
}