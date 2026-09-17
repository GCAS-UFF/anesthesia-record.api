using UFF.FichaAnestesica.Domain.Extensions;

namespace UFF.FichaAnestesica.Domain.Response
{
    public class InfusionPumpResponse
    {
        public int Id { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime Date { get; set; }
        public int DrugId { get; set; }
        public string DrugName { get; set; } = string.Empty;
        public decimal Rate { get; set; }
        public string RateUnit { get; set; } = string.Empty;
        public decimal VolumeMl { get; set; }
        public DateTime EndAt { get; set; }

        public static InfusionPumpResponse ToResponse(InfusionPump entity)
        {
            return new InfusionPumpResponse
            {
                Id = entity.Id,
                Time = entity.Time,
                Date = entity.Date,
                DrugId = entity.DrugId,
                DrugName = entity.Drug?.Description ?? string.Empty,
                Rate = entity.Rate,
                RateUnit = entity.RateUnit.GetDescription(),
                VolumeMl = entity.VolumeMl,
                EndAt = entity.EndAt
            };
        }
    }
}
