namespace UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord
{
    public class SurgeryCommand
    {
        public string Id { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string? Cid { get; set; }
        public TimeOnly Hora { get; set; }
        public bool IsPrimary { get; set; }
    }
}