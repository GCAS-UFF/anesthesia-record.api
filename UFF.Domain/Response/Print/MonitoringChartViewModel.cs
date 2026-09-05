namespace UFF.FichaAnestesica.Domain.Response.Print
{
    /// <summary>
    /// Representação já "geometrizada" (coordenadas prontas para desenhar) do gráfico de
    /// monitorização impresso, equivalente ao gráfico da tela de Monitorização do app.
    /// Coordenadas X/Y são unidades de um viewBox SVG (não pixels de tela).
    /// </summary>
    public class MonitoringChartViewModel
    {
        public bool HasData { get; set; }
        public List<MonitoringChartRow> Rows { get; set; } = new();
    }

    public class MonitoringChartRow
    {
        public string RangeLabel { get; set; } = string.Empty;
        public List<ChartAxisTick> Ticks { get; set; } = new();
        public List<VitalChartPoint> VitalPoints { get; set; } = new();
        public List<TemporalChartMarker> TemporalMarkers { get; set; } = new();
        public List<LaneChartMarker> SurgicalMarkers { get; set; } = new();
        public List<LaneChartMarker> AirwayMarkers { get; set; } = new();
        public List<LaneChartMarker> ClinicalMarkers { get; set; } = new();
        public List<LaneChartMarker> PositionMarkers { get; set; } = new();

        public bool HasLaneMarkers =>
            SurgicalMarkers.Count > 0 || AirwayMarkers.Count > 0 ||
            ClinicalMarkers.Count > 0 || PositionMarkers.Count > 0;
    }

    public class ChartAxisTick
    {
        public double X { get; set; }
        public string Label { get; set; } = string.Empty;
    }

    public enum VitalSeries { HeartRate, SystolicBp, DiastolicBp, MeanBp, Spo2, Temperature }

    public class VitalChartPoint
    {
        public double X { get; set; }
        public double Y { get; set; }
        public VitalSeries Series { get; set; }
    }

    public enum TemporalMarkerKind { AnesthesiaStart, AnesthesiaEnd, SurgeryStart, SurgeryEnd }

    public class TemporalChartMarker
    {
        public double X { get; set; }
        public TemporalMarkerKind Kind { get; set; }
        public string TimeLabel { get; set; } = string.Empty;
    }

    public class LaneChartMarker
    {
        public double X { get; set; }
        public int StackLevel { get; set; }
        public string TimeLabel { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
