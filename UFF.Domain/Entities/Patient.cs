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
        public string ExternalIdHuap { get; private set; }

        public CurrentLocation CurrentLocation { get; private set; }

        public List<Surgery> Surgeries => _surgeries;

        public static Patient Create(string patientId, string medicalRecordNumber, string fullName, DateTime birthDate, GenderEnum gender,
            double weightKg, int heightCm, CurrentLocation currentLocation)
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

        public void UpdateExternalCode(string externalCode)
            => this.ExternalIdHuap = externalCode;

        public void UpdatePatient(Patient patient)
        {
            FullName = patient.FullName;
            BirthDate = patient.BirthDate;
            Gender = patient.Gender;
            WeightKg = patient.WeightKg;
            HeightCm = patient.HeightCm;
            MedicalRecordNumber = patient.MedicalRecordNumber;
            ExternalIdHuap = patient.ExternalIdHuap;
        }

        public void SetCurrentLocation(CurrentLocation currentLocation)
            => this.CurrentLocation = currentLocation;

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
    }
}