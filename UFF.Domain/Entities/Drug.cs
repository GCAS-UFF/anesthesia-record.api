using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class Drug : Base
    {
        protected Drug() { }

        public string ExternalId { get; private set; }

        public string Description { get; private set; }

        public PresentationEnum Presentation { get; private set; }

        public UnitEnum DefaultUnit { get; private set; }

        public bool Active { get; private set; }

        public DateTime? LastSyncAt { get; private set; }

        public static Drug Create(string externalId, string description, PresentationEnum presentation)
        {
            return new Drug
            {
                ExternalId = externalId,
                Description = description,
                Presentation = presentation,
                Active = true,
                CreatedAt = DateTime.UtcNow,
                LastSyncAt = DateTime.UtcNow
            };
        }

        public void Update(string description, PresentationEnum presentation)
        {
            Description = string.IsNullOrWhiteSpace(description) ? Description : description;
            Presentation = presentation;
            Active = true;
            LastSyncAt = DateTime.UtcNow;
        }

        public void Disable()
        {
            Active = false;
            LastSyncAt = DateTime.UtcNow;
        }
    }
}