using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Response
{
    public class SurgeryResponse
    {
        public int Id { get; set; }
        public DateTime SurgeryDate { get; set; }
        public SurgeryStatus Status { get; set; }
        public SpecialtyResponse Specialty { get; set; }
        public SurgeryLocationResponse Location { get; set; }
        public List<ProcedureResponse> Procedures { get; set; }
    }
}