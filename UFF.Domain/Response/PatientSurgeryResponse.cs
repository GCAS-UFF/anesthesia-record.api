using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Response
{
    public class PatientSurgeryResponse
    {
        public int SurgeryId { get; set; }
        public string PatientId { get; set; }
        public DateTime SurgeryDate { get; set; }
        public string MedicalRecordNumber { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public double WeightKg { get; set; }
        public int HeightCm { get; set; }
        public int Age { get; set; }
        public PatientLocationResponse CurrentLocation { get; set; }
        public string CurrentLocationDescription { get; set; }
        public List<SurgeryResponse> Surgeries { get; set; }
        public List<ListAllergyDto> Allergies { get; set; }
        public ResponsibleResponse? Surgeon { get; set; }
        public SurgeryStatusEnum Status { get; set; }
        public bool IsPreAnesthesiaRecordDone { get; set; }

        public ResponsibleResponse? Assistant { get; set; }

        public ResponsibleResponse? FirstAnesthesiologist { get; set; }

        public ResponsibleResponse? SecondAnesthesiologist { get; set; }
        public DateTime? ExpectedAt { get; set; }
        public string Room { get; set; }
        public List<ProcedureResponse> Procedures { get; set; }
        public string PrimaryProcedure { get; set; }
    }
}
