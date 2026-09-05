namespace UFF.FichaAnestesica.Domain.Response.Print
{
    public class AnesthesiaRecordPrintViewModel
    {
        public PrintHospitalInfo Hospital { get; set; } = new();
        public AnesthesiaRecordResponse Record { get; set; } = null!;
        public PreAnesthesiaRecordResponse? PreAnesthesia { get; set; }
        public MonitoringRecordResponse? Monitoring { get; set; }
        public PrintFluidBalanceTotals FluidTotals { get; set; } = new();
        public MonitoringChartViewModel Chart { get; set; } = new();
        public DateTime PrintedAt { get; set; }
    }

    public class PrintHospitalInfo
    {
        public string Name { get; set; } = string.Empty;
        public string? Sector { get; set; }
        public string? Cnpj { get; set; }
        public string? Address { get; set; }
    }

    public class PrintFluidBalanceTotals
    {
        public decimal GainsMl { get; set; }
        public decimal LossesMl { get; set; }
        public decimal NetMl => GainsMl - LossesMl;
    }
}
