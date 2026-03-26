namespace UFF.FichaAnestesica.Domain.Entities
{
    public class Base 
    {
        public int Id { get; set; }
        public DateTime RegisteringDate { get; private set; }
        public DateTime UpdateDate { get; private set; }
    }
}
