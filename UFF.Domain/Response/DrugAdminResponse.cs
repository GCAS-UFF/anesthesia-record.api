namespace UFF.FichaAnestesica.Domain.Response
{
    public class DrugAdminResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string DefaultUnit { get; set; }
        public bool Active { get; set; }
        public int CategoryId { get; set; }
        public string CategoryLabel { get; set; }
    }
}
