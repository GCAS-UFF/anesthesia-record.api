using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class Patient : Base
    {
        private Patient()
        {
        }

        public string MedicalRecordNumber { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderEnum Gender { get; set; }
        public double WeightKg { get; set; }
        public int HeightCm { get; set; }

        public CurrentLocation CurrentLocation { get; set; }

        public List<Surgery> Surgeries { get; set; }
    }
}