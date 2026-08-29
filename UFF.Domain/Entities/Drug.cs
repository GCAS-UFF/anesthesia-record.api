using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class Drug : Base
    {
        protected Drug() { }

        public string ExternalId { get; private set; }
        public string Description { get; private set; }
        public string DefaultUnit { get; private set; }
        public bool Active { get; private set; }
        public DateTime? LastSyncAt { get; private set; }
        public DrugCategoryEnum Category { get; private set; }

        public static Drug Create(string externalId, string description, string unit)
        {
            return new Drug
            {
                ExternalId = externalId,
                Description = description,
                Active = true,
                DefaultUnit = unit,
                CreatedAt = DateTime.UtcNow,
                LastSyncAt = DateTime.UtcNow,
                Category = DrugCategoryEnum.Outros
            };
        }

        public void Update(string description, string unity)
        {
            Description = string.IsNullOrWhiteSpace(description) ? Description : description;
            Active = true;
            DefaultUnit = unity;
            LastSyncAt = DateTime.UtcNow;
        }

        public void Disable()
        {
            Active = false;
            LastSyncAt = DateTime.UtcNow;
        }

        public void UpdateCategory(DrugCategoryEnum category)
        {
            Category = category;
        }
    }
}