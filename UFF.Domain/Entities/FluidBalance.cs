namespace UFF.FichaAnestesica.Domain.Entities
{
    public class FluidBalance
    {
        public DateTime Timestamp { get; set; }
        public string BalanceType { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int VolumeMl { get; set; }
    }
}