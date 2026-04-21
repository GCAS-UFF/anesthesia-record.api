using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class Surgery : Base
    {
        private Surgery() { }

        public string SurgeryId { get; set; }
        public DateTime SurgeryDate { get; private set; }
        public SurgeryStatus Status { get; private set; }
        public string PatientId { get; private set; }
        public Patient Patient { get; private set; }
        public Specialty Specialty { get; private set; }
        public SurgeryLocation Location { get; private set; }
        public List<Procedure> Procedures { get; private set; }

        public static Surgery Create(string surgeryId, DateTime surgeryDate, SurgeryStatus status, string patientId, Specialty specialty, SurgeryLocation location)
        {
            return new Surgery
            {
                SurgeryId = surgeryId,
                SurgeryDate = surgeryDate,
                Status = status,
                PatientId = patientId,
                Specialty = specialty,
                Location = location,
                Procedures = []
            };
        }

        public void ReplaceProcedures(IEnumerable<Procedure> procedures)
        {
            Procedures.Clear();

            foreach (var proc in procedures)
            {
                Procedures.Add(proc);
            }
        }
        public void SyncProcedures(IEnumerable<Procedure> incoming)
        {
            var existingDict = Procedures.ToDictionary(p => p.ExternalId);

            foreach (var incomingProc in incoming)
            {
                if (!existingDict.TryGetValue(incomingProc.ExternalId, out var existing))
                {
                    Procedures.Add(incomingProc);
                }
                else
                {
                    existing.Sync(incomingProc);
                }
            }

            var incomingIds = incoming.Select(p => p.ExternalId).ToHashSet();

            var toRemove = Procedures
                .Where(p => !incomingIds.Contains(p.ExternalId))
                .ToList();

            foreach (var remove in toRemove)
            {
                Procedures.Remove(remove);
            }
        }

        public void Update(DateTime surgeryDate, SurgeryStatus status, Specialty specialty, SurgeryLocation location)
        {
            SurgeryDate = surgeryDate;
            Status = status;
            Specialty = specialty;
            Location = location;
        }
        public void SetSpecialty(Specialty specialty)
        {
            Specialty = specialty;
        }

        public void AddProcedure(Procedure procedure)
        {
            if (!Procedures.Any(p => p.ExternalId == procedure.ExternalId))
                Procedures.Add(procedure);
        }
    }
}