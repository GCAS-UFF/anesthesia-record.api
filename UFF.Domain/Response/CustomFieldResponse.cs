using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Domain.Response
{
    public class CustomFieldResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        public static CustomFieldResponse ToResponse(
            CustomField entity)
        {
            return new CustomFieldResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Value = entity.Value
            };
        }
    }
}