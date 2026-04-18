namespace UFF.FichaAnestesica.Domain.Entities
{
    public class Base 
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime RegisteringDate { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdate { get; set; } = DateTime.UtcNow;
    }
}
