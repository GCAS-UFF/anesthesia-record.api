namespace UFF.FichaAnestesica.Domain.Entities
{
    public class Specialty : Base
    {
        private Specialty() { }

        public string Code { get; private set; }
        public string Description { get; private set; }

        public void Sync(Specialty incoming)
        {
            Description = incoming.Description;
        }

        public static Specialty Create(string code, string description)
        {
            return new Specialty
            {
                Code = code,
                Description = description
            };
        }
    }
}
