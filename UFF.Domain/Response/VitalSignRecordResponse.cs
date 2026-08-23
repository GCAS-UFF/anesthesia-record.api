using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Domain.Response
{
    public class VitalSignRecordResponse
    {
        public int Id { get; set; }
        public DateTime Date { get; private set; }
        public TimeSpan Time { get; private set; }

        public int? SystolicBloodPressure { get; set; }

        public int? DiastolicBloodPressure { get; set; }

        public int? MeanArterialPressure { get; set; }

        public int? HeartRate { get; set; }

        public int? Spo2 { get; set; }

        public int? Etco2 { get; set; }

        public decimal? Temperature { get; set; }

        public int? Bis { get; set; }

        public decimal? Pvc { get; set; }

        public decimal? Pcap { get; set; }

        public List<CustomFieldResponse> CustomFields { get; set; } = new();

        public static VitalSignRecordResponse ToResponse(VitalSignRecord entity)
        {
            return new VitalSignRecordResponse
            {
                Id = entity.Id,
                Time = entity.Time,
                Date = entity.Date,
                SystolicBloodPressure = entity.SystolicBloodPressure,
                DiastolicBloodPressure = entity.DiastolicBloodPressure,
                MeanArterialPressure = entity.MeanArterialPressure,
                HeartRate = entity.HeartRate,
                Spo2 = entity.Spo2,
                Etco2 = entity.Etco2,
                Temperature = entity.Temperature,
                Bis = entity.Bis,
                Pvc = entity.Pvc,
                Pcap = entity.Pcap,
                CustomFields = entity.CustomFields.Select(CustomFieldResponse.ToResponse).ToList()
            };
        }
    }
}