namespace UFF.FichaAnestesica.Domain.Response
{
    public class PatientSurgeryResponse
    {
        public string PatientId { get; set; }
        public int Id { get; set; }
        public string MedicalRecordNumber { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public double WeightKg { get; set; }
        public int HeightCm { get; set; }
        public int Age { get; set; } 
        public PatientLocationResponse CurrentLocation { get; set; }
        public List<SurgeryResponse> Surgeries { get; set; }
    }
}
