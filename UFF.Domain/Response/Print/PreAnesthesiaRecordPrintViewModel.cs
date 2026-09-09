namespace UFF.FichaAnestesica.Domain.Response.Print
{
    public class PreAnesthesiaRecordPrintViewModel
    {
        public PrintHospitalInfo Hospital { get; set; } = new();
        public AnesthesiaRecordResponse Record { get; set; } = null!;
        public PreAnesthesiaRecordResponse PreAnesthesia { get; set; } = null!;
        public DateTime PrintedAt { get; set; }
    }
}
