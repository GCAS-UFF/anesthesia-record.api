using System;
using UFF.FichaAnestesica.Domain.Dto;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class Paciente : Base
    {
        public Paciente() { }

        public string ExternalIdHuap { get; set; } = string.Empty;
        public string Prontuario { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; } = string.Empty;
        public float? PesoKg { get; set; }
        public float? AlturaCm { get; set; }
    }
}
