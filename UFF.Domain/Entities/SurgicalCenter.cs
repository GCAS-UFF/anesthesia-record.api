namespace UFF.FichaAnestesica.Domain.Entities
{
    public class SurgicalCenter : Base
    {
        private SurgicalCenter() { }

        public string Code { get; private set; }
        public string Description { get; private set; }

        public static SurgicalCenter Create(string code, string description)
        {
            return new SurgicalCenter
            {
                Code = code,
                Description = description
            };
        }
    }
}
