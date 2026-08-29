namespace UFF.FichaAnestesica.Domain.Entities
{
    public class EventType : Base
    {
        protected EventType() { }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Active { get; private set; }

        public static EventType Create(string name, string description)
        {
            return new EventType
            {
                Name = name,
                Description = description,
                Active = true
            };
        }

        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void Disable()
        {
            Active = false;
        }

        public void Enable()
        {
            Active = true;
        }
    }
}
