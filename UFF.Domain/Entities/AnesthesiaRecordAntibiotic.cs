using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class AnesthesiaRecordAntibiotic
    {
        private AnesthesiaRecordAntibiotic() { }

        public int Id { get; private set; }

        public int AnesthesiaRecordId { get; private set; }
        public AnesthesiaRecord AnesthesiaRecord { get; private set; } = default!;

        public int MedicationId { get; private set; }

        public string MedicationName { get; private set; } = string.Empty;

        public string Name { get; private set; } = string.Empty;

        public string Dose { get; private set; } = string.Empty;

        public string Route { get; private set; } = string.Empty;

        public TimeOnly Time { get; private set; }

        public bool HasBooster { get; private set; }

        public List<AnesthesiaRecordAntibioticBooster> Boosters { get; private set; } = [];

        public static AnesthesiaRecordAntibiotic Create(AntibioticCommand command)
        {
            var entity = new AnesthesiaRecordAntibiotic();

            entity.MedicationId = command.MedicationId;
            entity.MedicationName = command.MedicationName;
            entity.Name = command.Name;
            entity.Dose = command.Dose;
            entity.Route = command.Route;
            entity.Time = command.Time;
            entity.HasBooster = command.HasBooster;

            entity.Boosters = command.Boosters
                .Select(AnesthesiaRecordAntibioticBooster.Create)
                .ToList();

            return entity;
        }

        public void Update(AntibioticCommand command)
        {
            MedicationId = command.MedicationId;
            MedicationName = command.MedicationName;
            Name = command.Name;
            Dose = command.Dose;
            Route = command.Route;
            Time = command.Time;
            HasBooster = command.HasBooster;

            Boosters.Clear();

            foreach (var booster in command.Boosters)
                Boosters.Add(AnesthesiaRecordAntibioticBooster.Create(booster));
        }
    }
}