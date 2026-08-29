using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Response;

namespace UFF.FichaAnestesica.Service.Mappers
{
    public static class EventTypeResponseMapper
    {
        public static EventTypeResponse Map(EventType eventType)
        {
            if (eventType == null)
                return null;

            return new EventTypeResponse
            {
                Id = eventType.Id,
                Name = eventType.Name,
                Description = eventType.Description,
                Active = eventType.Active
            };
        }

        public static List<EventTypeResponse> Map(List<EventType> eventTypes)
        {
            if (eventTypes == null)
                return null;

            return eventTypes.Select(Map).ToList();
        }
    }
}
