namespace UFF.FichaAnestesica.Domain.Response.Print
{  
    public class MonitoringChartViewModel
    {
        public bool HasData { get; set; }
        public List<MonitoringChartRow> Rows { get; set; } = new();
    }

    public class MonitoringChartRow
    {
        public string RangeLabel { get; set; } = string.Empty;
        public List<ChartAxisTick> Ticks { get; set; } = new();
        public List<ChartValueTick> ValueTicks { get; set; } = new();
        public List<VitalChartPoint> VitalPoints { get; set; } = new();
        public List<HeartRateLabelMarker> HeartRateLabels { get; set; } = new();
        public List<TemporalChartMarker> TemporalMarkers { get; set; } = new();
        public List<LaneChartMarker> SurgicalMarkers { get; set; } = new();
        public List<LaneChartMarker> AirwayMarkers { get; set; } = new();
        public List<LaneChartMarker> ClinicalMarkers { get; set; } = new();
        public List<LaneChartMarker> PositionMarkers { get; set; } = new();

        public bool HasLaneMarkers =>
            SurgicalMarkers.Count > 0 || AirwayMarkers.Count > 0 ||
            ClinicalMarkers.Count > 0 || PositionMarkers.Count > 0;
    }

 
    public interface IStackableMarker
    {
        double X { get; }
        int StackLevel { get; set; }
    }

    public class ChartAxisTick
    {
        public double X { get; set; }
        public string Label { get; set; } = string.Empty;

        public bool IsMajor { get; set; }
    }

    
    public class ChartValueTick
    {
        public double Y { get; set; }
        public double Value { get; set; }
        public string Label { get; set; } = string.Empty;
    }

    
    public class HeartRateLabelMarker : IStackableMarker
    {
        public double X { get; set; }
        public double Y { get; set; }
        public int Value { get; set; }
        public int StackLevel { get; set; }
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

    public class LaneChartMarker : IStackableMarker
    {
        public double X { get; set; }
        public int StackLevel { get; set; }
        public string TimeLabel { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
