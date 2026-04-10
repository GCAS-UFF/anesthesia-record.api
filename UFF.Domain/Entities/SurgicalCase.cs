using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class SurgicalCase : Base
    {
        public SurgicalCase() { }

        public string ExternalIdHuap { get; set; } = string.Empty;
        
        public Guid PatientId { get; set; }
        
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; } = null!;
        
        public DateTime SurgeryDate { get; set; }
        public string ProposedProcedure { get; set; } = string.Empty;
        public string Room { get; set; } = string.Empty;
        public string Bed { get; set; } = string.Empty;
        public string Status { get; set; } = "waiting"; 
        
        public string Surgeon { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
    }
}