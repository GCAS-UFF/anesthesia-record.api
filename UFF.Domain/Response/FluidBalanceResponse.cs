using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Response
{
    public class FluidBalanceResponse
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        public FluidBalanceTypeEnum Type { get; set; }

        public FluidCategoryEnum Category { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal VolumeMl { get; set; }

        public static FluidBalanceResponse ToResponse(FluidBalance entity)
        {
            return new FluidBalanceResponse
            {
                Id = entity.Id,
                Type = entity.Type,
                Category = entity.Category,
                VolumeMl = entity.VolumeMl
            };
        }
    }
}