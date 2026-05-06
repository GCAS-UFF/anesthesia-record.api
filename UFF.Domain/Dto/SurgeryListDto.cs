using System;

namespace UFF.FichaAnestesica.Domain.Dto
{
    public class SurgeryListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string BirthDate { get; set; } = string.Empty;
        public string MedicalRecord { get; set; } = string.Empty;
        public string Room { get; set; } = string.Empty;
        public string Procedure { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}