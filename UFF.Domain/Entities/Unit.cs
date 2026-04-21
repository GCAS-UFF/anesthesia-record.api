namespace UFF.FichaAnestesica.Domain.Entities
{
    public class Unit : Base
    {
        private Unit() { }

        public string Code { get; private set; }
        public string Description { get; private set; }

        public static Unit Create(string code, string description)
        {
            return new Unit
            {
                Code = code,
                Description = description
            };
        }

        public void Sync(Unit incoming)
        {
            if (incoming == null)
                return;

            Description = incoming.Description;
        }
    }
}