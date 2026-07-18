namespace UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord
{
    public class ProfessionalCommand
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Registration { get; set; } = default!;
    }
}