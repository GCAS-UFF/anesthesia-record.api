using System;

namespace UFF.FichaAnestesica.Domain.Dto
{
    public class CirurgiaListaDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Nascimento { get; set; } = string.Empty;
        public string Prontuario { get; set; } = string.Empty;
        public string Sala { get; set; } = string.Empty;
        public string Procedimento { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
