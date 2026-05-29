namespace UFF.FichaAnestesica.Domain.Entities
{
    public class VitalSignRecord : Base
    {
        public DateTime Timestamp { get; private set; }
        public int? SystolicBloodPressure { get; private set; }
        public int? DiastolicBloodPressure { get; private set; }
        public int? MeanArterialPressure { get; private set; }
        public int? HeartRate { get; private set; }
        public int? Spo2 { get; private set; }
        public int? Etco2 { get; private set; }
        public decimal? Temperature { get; private set; }
        public int? Bis { get; private set; }
        public decimal? Pvc { get; private set; }
        public decimal? Pcap { get; private set; }
        public int MonitoringRecordId { get; private set; }
        public MonitoringRecord MonitoringRecord { get; private set; }
        public List<CustomField> CustomFields { get; private set; } = new();
    }  
}