namespace UFF.FichaAnestesica.Domain.Entities
{
    public class VitalSignRecord
    {
        public DateTime Timestamp { get; set; }
        public int? SystolicBloodPressure { get; set; }
        public int? DiastolicBloodPressure { get; set; }
        public int? MeanArterialPressure { get; set; }
        public int? HeartRate { get; set; }
        public int? Spo2 { get; set; }
        public int? Etco2 { get; set; }
        public double? Temperature { get; set; }
        public int? Bis { get; set; }
        public double? Pvc { get; set; }
        public double? Pcap { get; set; }
        public List<CustomField> CustomFields { get; set; } = new();
    }  
}