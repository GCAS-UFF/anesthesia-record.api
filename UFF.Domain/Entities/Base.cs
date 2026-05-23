namespace UFF.FichaAnestesica.Domain.Entities
{
    public abstract class Base 
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdate { get; set; }
        public DateTime? LastLoginAt { get; protected set; }
        public DateTime? LastSyncAt { get; protected set; } 
    }
}
