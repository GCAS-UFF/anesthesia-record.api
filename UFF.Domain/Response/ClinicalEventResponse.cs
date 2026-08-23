using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Response
{
    public class ClinicalEventResponse
    {
        public TimeSpan Time { get; set; }
        public DateTime Date { get; set; }
        public ClinicalEventTypeEnum EventType { get; private set; }
        public string? Observations { get; private set; }

        public static ClinicalEventResponse ToResponse(ClinicalEvent entity)
        {
            return new ClinicalEventResponse
            {
                EventType = entity.EventType,
                Time = entity.Time,
                Date = entity.Date,
                Observations = entity.Observations
            };
        }
    }
}