namespace UFF.FichaAnestesica.Domain.Response.Reports
{
    public class ReportPdfKpi
    {
        public string Label { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    public class ReportPdfChart
    {
        public string Title { get; set; } = string.Empty;

        // "bar" | "doughnut" | "line"
        public string Type { get; set; } = "bar";
        public List<string> Labels { get; set; } = [];
        public List<decimal> Values { get; set; } = [];
    }

    public class ReportPdfTable
    {
        public List<string> Headers { get; set; } = [];
        public List<List<string>> Rows { get; set; } = [];
    }

    public class ReportPdfViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string Subtitle { get; set; } = string.Empty;
        public string PeriodLabel { get; set; } = string.Empty;
        public string FiltersLabel { get; set; } = string.Empty;
        public DateTime GeneratedAt { get; set; }
        public List<ReportPdfKpi> Kpis { get; set; } = [];
        public List<ReportPdfChart> Charts { get; set; } = [];
        public ReportPdfTable? Table { get; set; }
    }
}
