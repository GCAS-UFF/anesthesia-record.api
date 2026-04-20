namespace UFF.FichaAnestesica.Domain.Entities
{
    public class Procedure : Base
    {
        private Procedure()
        {
        }

        public string Id { get; set; }
        public string Description { get; set; }
        public string Cid { get; set; }
        public bool IsPrimary { get; set; }
    }
}
