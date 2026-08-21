using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Domain.Response
{
    public class PreAnesthesiaSurgeryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }

        public static PreAnesthesiaSurgeryResponse ToResponse(PreAnesthesiaSurgery entity)
        {
            return new PreAnesthesiaSurgeryResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                IsPrimary = entity.IsPrimary
            };
        }
    }
}
