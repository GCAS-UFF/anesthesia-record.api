namespace UFF.FichaAnestesica.Domain.Entities
{
    public abstract class Base 
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime LastUpdate { get; protected set; }
       
    }
}
