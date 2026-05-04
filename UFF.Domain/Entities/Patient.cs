using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class Patient : Base
    {
        private readonly List<Surgery> _surgeries = new();

        private Patient() { }

        public string PatientId { get; private set; }
        public string MedicalRecordNumber { get; private set; }
        public string FullName { get; private set; }
        public DateTime BirthDate { get; private set; }
        public GenderEnum Gender { get; private set; }
        public double WeightKg { get; private set; }
        public int HeightCm { get; private set; }

        public CurrentLocation CurrentLocation { get; private set; }

        public List<Surgery> Surgeries => _surgeries;

        public static Patient Create(
            string patientId,
            string medicalRecordNumber,
            string fullName,
            DateTime birthDate,
            GenderEnum gender,
            double weightKg,
            int heightCm,
            CurrentLocation currentLocation)
        {
            return new Patient
            {
                PatientId = patientId,
                MedicalRecordNumber = medicalRecordNumber,
                FullName = fullName,
                BirthDate = birthDate,
                Gender = gender,
                WeightKg = weightKg,
                HeightCm = heightCm,
                CurrentLocation = currentLocation
            };
        }

        public void Sync(Patient incoming)
        {
            UpdateBasicInfo(incoming);
            SyncLocation(incoming.CurrentLocation);
            SyncSurgeries(incoming.Surgeries);
        }

   
        private void UpdateBasicInfo(Patient incoming)
        {
            FullName = incoming.FullName;
            MedicalRecordNumber = incoming.MedicalRecordNumber;
            BirthDate = incoming.BirthDate;
            Gender = incoming.Gender;
            WeightKg = incoming.WeightKg;
            HeightCm = incoming.HeightCm;
        }

       
        private void SyncLocation(CurrentLocation incoming)
        {
            if (incoming == null)
            {
                CurrentLocation = null;
                return;
            }

            if (CurrentLocation == null)
            {
                CurrentLocation = incoming;
                return;
            }

            CurrentLocation.Sync(incoming);
        }

        public void SyncSurgery(Surgery incoming)
        {
            var existing = _surgeries
                .FirstOrDefault(s => s.Id == incoming.Id);

            if (existing == null)
            {
                _surgeries.Add(incoming);
                return;
            }

            existing.Update(
                incoming.SurgeryDate,
                incoming.Status,
                incoming.Specialty,
                incoming.Location
            );
        }

        public void ReplaceSurgeries(List<Surgery> surgeries)
        {
            Surgeries.Clear();

            foreach (var s in surgeries)
                Surgeries.Add(s);
        }

        private void SyncSurgeries(IEnumerable<Surgery> incoming)
        {
            var existingDict = _surgeries.ToDictionary(s => s.Id);

            foreach (var surgery in incoming)
            {
                if (!existingDict.TryGetValue(surgery.Id, out var existing))
                {
                    _surgeries.Add(surgery);
                }
                else
                {
                    existing.SyncProcedures(surgery.Procedures);
                }
            }

            var incomingIds = incoming.Select(s => s.SurgeryId).ToHashSet();

            var toRemove = _surgeries
                .Where(s => !incomingIds.Contains(s.SurgeryId))
                .ToList();

            foreach (var remove in toRemove)
            {
                _surgeries.Remove(remove);
            }
        }
    }
}