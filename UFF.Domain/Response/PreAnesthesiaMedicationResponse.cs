using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Domain.Response
{
    public class PreAnesthesiaMedicationResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Dose { get; set; }
        public string? Route { get; set; }
        public string? Frequency { get; set; }

        public static PreAnesthesiaMedicationResponse ToResponse(PreAnesthesiaMedication entity)
        {
            return new PreAnesthesiaMedicationResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Dose = entity.Dose,
                Route = entity.Route,
                Frequency = entity.Frequency
            };
        }
    }
}
