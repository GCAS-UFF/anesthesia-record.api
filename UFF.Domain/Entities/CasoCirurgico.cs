using System;
using System.ComponentModel.DataAnnotations.Schema;
using UFF.FichaAnestesica.Domain.Dto;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class CasoCirurgico : Base
    {
        public CasoCirurgico() { }

        public string ExternalIdHuap { get; set; } = string.Empty;
        
        public Guid PacienteId { get; set; }
        
        [ForeignKey("PacienteId")]
        public virtual Paciente Paciente { get; set; } = null!;
        
        public DateTime DataCirurgia { get; set; }
        public string ProcedimentoProposto { get; set; } = string.Empty;
        public string Sala { get; set; } = string.Empty;
        public string Leito { get; set; } = string.Empty;
        public string Status { get; set; } = "espera"; 
        // espera / realizado / ocupado (conforme interface frontend)
        
        public string Cirurgiao { get; set; } = string.Empty;
        public string Especialidade { get; set; } = string.Empty;
    }
}
