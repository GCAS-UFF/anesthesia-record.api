using UFF.FichaAnestesica.Domain.Commands.PreAnesthesiaRecord;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class PreAnesthesiaMedication : Base
    {
        private PreAnesthesiaMedication() { }

        public string Name { get; private set; } = default!;
        public string? Dose { get; private set; }
        public string? Route { get; private set; }
        public string? Frequency { get; private set; }

        public int PreAnesthesiaRecordId { get; private set; }
        public PreAnesthesiaRecord PreAnesthesiaRecord { get; private set; } = default!;

        public static PreAnesthesiaMedication Create(PreAnesthesiaMedicationCommand command)
        {
            return new PreAnesthesiaMedication
            {
                Name = command.Name,
                Dose = command.Dose,
                Route = command.Route,
                Frequency = command.Frequency,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void SetPreAnesthesiaRecord(PreAnesthesiaRecord preAnesthesiaRecord)
        {
            PreAnesthesiaRecord = preAnesthesiaRecord;
        }
    }
}
