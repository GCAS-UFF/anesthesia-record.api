using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class Surgery : Base
    {

        private Surgery()
        {                
        }

        public string Id { get; set; }
        public DateTime SurgeryDate { get; set; }
        public SurgeryStatus Status { get; set; }

        public Specialty Specialty { get; set; }
        public SurgeryLocation Location { get; set; }

        public List<Procedure> Procedures { get; set; }
    }
}