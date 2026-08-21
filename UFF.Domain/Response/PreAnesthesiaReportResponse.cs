using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Domain.Response
{
    public class PreAnesthesiaReportResponse
    {
        public int Id { get; set; }
        public string? Specialty { get; set; }
        public string? Description { get; set; }

        public static PreAnesthesiaReportResponse ToResponse(PreAnesthesiaReport entity)
        {
            return new PreAnesthesiaReportResponse
            {
                Id = entity.Id,
                Specialty = entity.Specialty?.ToString(),
                Description = entity.Description
            };
        }
    }
}
