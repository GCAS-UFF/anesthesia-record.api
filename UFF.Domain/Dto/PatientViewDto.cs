
namespace UFF.FichaAnestesica.Domain.Dto
{
    public class PatientViewDto
    {
        public string Id { get; set; }
        public string MedicalRecordNumber { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public double WeightKg { get; set; }
        public int HeightCm { get; set; }

        public string UnitCode { get; set; }
        public string UnitDescription { get; set; }
        public string Bed { get; set; }
        public string Floor { get; set; }
        public string Room { get; set; }


        public string SurgeryId { get; set; }
        public DateTime SurgeryDate { get; set; }
        public string SurgeryStatus { get; set; }

        public string SpecialtyCode { get; set; }
        public string SpecialtyDescription { get; set; }

        public string SurgicalCenterCode { get; set; }
        public string SurgicalCenterDescription { get; set; }
        public string SurgeryRoom { get; set; }

        public string ProcedureId { get; set; }
        public string ProcedureDescription { get; set; }
        public string ProcedureCid { get; set; }
        public bool IsPrimaryProcedure { get; set; }
    }
}