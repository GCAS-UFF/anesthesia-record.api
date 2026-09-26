namespace UFF.FichaAnestesica.Domain.Commands.PreAnesthesiaRecord
{
    public class PreAnesthesiaLabExamCommand
    {
        public List<PreAnesthesiaLabResultCommand> Results { get; set; } = new();
    }

    public class PreAnesthesiaLabResultCommand
    {
        public string Analyte { get; set; } = string.Empty;
        public decimal? Value { get; set; }
    }
}
