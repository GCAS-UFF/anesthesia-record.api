using System;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class Patient : Base
    {
        public Patient() { }

        public string ExternalIdHuap { get; set; } = string.Empty;
        public string MedicalRecord { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = string.Empty;
        public float? WeightKg { get; set; }
        public float? HeightCm { get; set; }
    }
}