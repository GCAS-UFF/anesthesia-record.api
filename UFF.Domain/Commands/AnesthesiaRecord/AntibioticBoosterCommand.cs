namespace UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord
{
    public class AntibioticBoosterCommand
    {
        public int MedicationId { get; set; }

        public string MedicationName { get; set; } = default!;

        public string Name { get; set; } = default!;

        public string Dose { get; set; } = default!;

        public string Route { get; set; } = default!;

        public TimeOnly Time { get; set; }
    }
}