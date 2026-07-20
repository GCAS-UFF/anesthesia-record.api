namespace UFF.FichaAnestesica.Domain.Entities
{
    public class Procedure : Base
    {
        protected Procedure() { }
        public string ExternalId { get; private set; } = null!;
        public string Code { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public string? Cid { get; private set; }
        public bool Active { get; private set; }
        public DateTime? LastSyncAt { get; private set; }
        public ICollection<AnesthesiaRecordSurgery> AnesthesiaRecords { get; private set; } = [];

        public static Procedure Create(string externalId, string code, string description, string? cid)
        {
            return new Procedure
            {
                ExternalId = externalId,
                Code = code,
                Description = description,
                Cid = cid,
                Active = true,
                CreatedAt = DateTime.UtcNow,
                LastSyncAt = DateTime.UtcNow
            };
        }

        public void Update(string code, string description, string? cid)
        {
            Code = code;
            Description = description;
            Cid = cid;
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