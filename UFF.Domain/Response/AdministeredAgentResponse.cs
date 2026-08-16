using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Response
{
    public class AdministeredAgentResponse
    {
        public int Id { get; set; }

        public TimeSpan Time { get; set; }
        public DateTime Date { get; set; }

        public int DrugId { get; set; }

        public string DrugName { get; set; } = string.Empty;

        public decimal Dose { get; set; }

        public string Unit { get; set; }

        public AdministrationRouteEnum Route { get; set; }

        public static AdministeredAgentResponse ToResponse(AdministeredAgent entity)
        {
            return new AdministeredAgentResponse
            {
                Id = entity.Id,
                Time = entity.Time,
                DrugId = entity.DrugId,
                DrugName = entity.Drug?.Description ?? string.Empty,
                Dose = entity.Dose,
               // Unit = entity.Unit,
                Route = entity.Route
            };
        }
    }
}