using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class AnesthesiaRecordAntibioticBooster
    {
        private AnesthesiaRecordAntibioticBooster() { }

        public int Id { get; private set; }

        public int AnesthesiaRecordAntibioticId { get; private set; }
        public AnesthesiaRecordAntibiotic Antibiotic { get; private set; } = default!;

        public int MedicationId { get; private set; }

        public string MedicationName { get; private set; } = string.Empty;

        public string Name { get; private set; } = string.Empty;

        public string Dose { get; private set; } = string.Empty;

        public string Route { get; private set; } = string.Empty;

        public TimeOnly Time { get; private set; }

        public static AnesthesiaRecordAntibioticBooster Create(AntibioticBoosterCommand command)
        {
            return new AnesthesiaRecordAntibioticBooster
            {
                MedicationId = command.MedicationId,
                MedicationName = command.MedicationName,
                Name = command.Name,
                Dose = command.Dose,
                Route = command.Route,
                Time = command.Time
            };
        }

        public void Update(AntibioticBoosterCommand command)
        {
            MedicationId = command.MedicationId;
            MedicationName = command.MedicationName;
            Name = command.Name;
            Dose = command.Dose;
            Route = command.Route;
            Time = command.Time;
        }
    }
}