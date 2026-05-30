using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Response
{
    public class ClinicalEventResponse
    {
        public DateTime Timestamp { get; private set; }
        public ClinicalEventTypeEnum EventType { get; private set; }
        public string Description { get; private set; }
        public string? Observations { get; private set; }

        public static ClinicalEventResponse ToResponse(ClinicalEvent entity)
        {
            return new ClinicalEventResponse
            {
               Description = entity.Description,
               EventType = entity.EventType,
               Timestamp = entity.Timestamp,
               Observations = entity.Observations
            };
        }
    }
}