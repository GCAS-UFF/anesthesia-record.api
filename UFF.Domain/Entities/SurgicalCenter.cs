namespace UFF.FichaAnestesica.Domain.Entities
{
    public class SurgicalCenter : Base
    {
        private SurgicalCenter() { }

        public void Sync(SurgicalCenter incoming)
        {
            if (incoming == null)
                return;

            // ⚠️ NUNCA mexer em Id nem Code (chave natural)
            // Id = incoming.Id; ❌
            // Code = incoming.Code; ❌

            Description = incoming.Description;
        }

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
