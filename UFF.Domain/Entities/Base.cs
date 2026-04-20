namespace UFF.FichaAnestesica.Domain.Entities
{
    public class Base 
    {
        public Guid Id { get; set; }
        public DateTime RegisteringDate { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
