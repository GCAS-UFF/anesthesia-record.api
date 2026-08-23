using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class VitalSignRecord : Base
    {
        public DateTime Date { get; private set; }
        public TimeSpan Time { get; private set; }
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

        public static VitalSignRecord Create(VitalSignRecordCommand command)

        {
            var vitalSignRecord = new VitalSignRecord
            {
                Time = command.Time,
                Date = command.Date,
                SystolicBloodPressure = command.SystolicBloodPressure,
                DiastolicBloodPressure = command.DiastolicBloodPressure,
                MeanArterialPressure = command.MeanArterialPressure,
                HeartRate = command.HeartRate,
                Spo2 = command.Spo2,
                Etco2 = command.Etco2,
                Temperature = command.Temperature,
                Bis = command.Bis,
                Pvc = command.Pvc,
                Pcap = command.Pcap,
                CreatedAt = DateTime.UtcNow
            };

            if (command.CustomFields != null &&
                command.CustomFields.Any())
            {
                vitalSignRecord.CustomFields = command.CustomFields
                    .Select(CustomField.Create)
                    .ToList();
            }

            return vitalSignRecord;
        }

        public void SetMonitoringRecord(MonitoringRecord monitoringRecord)
        {
            MonitoringRecord = monitoringRecord;
        }

        public void Update(VitalSignRecordCommand command)
        {
            Date = command.Date;
            Time = command.Time;
            SystolicBloodPressure = command.SystolicBloodPressure;
            DiastolicBloodPressure = command.DiastolicBloodPressure;
            MeanArterialPressure = command.MeanArterialPressure;
            HeartRate = command.HeartRate;
            Spo2 = command.Spo2;
            Etco2 = command.Etco2;
            Temperature = command.Temperature;
            Bis = command.Bis;
            Pvc = command.Pvc;
            Pcap = command.Pcap;

            CustomFields.Clear();

            if (command.CustomFields != null)
            {
                foreach (var customField in command.CustomFields)
                {
                    CustomFields.Add(CustomField.Create(customField));
                }
            }

            LastUpdate = DateTime.UtcNow;
        }
    }  
}