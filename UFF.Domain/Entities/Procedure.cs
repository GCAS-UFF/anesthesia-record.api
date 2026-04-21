namespace UFF.FichaAnestesica.Domain.Entities
{
    public class Procedure : Base
    {
        private Procedure() { }

        public string ExternalId { get; private set; }
        public string Description { get; private set; }
        public string Cid { get; private set; }
        public bool IsPrimary { get; private set; }


        public static Procedure Create(string externalId, string description, string cid, bool isPrimary)
        {
            return new Procedure
            {
                ExternalId = externalId,
                Description = description,
                Cid = cid,
                IsPrimary = isPrimary
            };
        }

        public void Update(string description, string cid, bool isPrimary)
        {
            Description = description;
            Cid = cid;
            IsPrimary = isPrimary;
        }

        public void Sync(Procedure incoming)
        {
            if (incoming == null)
                return;

            Update(incoming.Description, incoming.Cid, incoming.IsPrimary);
        }
    }
}