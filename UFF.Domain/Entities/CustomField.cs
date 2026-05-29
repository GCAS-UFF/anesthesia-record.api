using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class CustomField : Base
    {
        public string Name { get; private set; } = string.Empty;
        public string Value { get; private set; } = string.Empty;

        public static CustomField Create(CustomFieldCommand command)
        {
            return new CustomField
            {
                Name = command.Name,
                Value = command.Value,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void Update(CustomFieldCommand command)
        {
            Name = command.Name;
            Value = command.Value;

            LastUpdate = DateTime.UtcNow;
        }
    }
}