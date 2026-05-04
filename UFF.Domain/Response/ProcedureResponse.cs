namespace UFF.FichaAnestesica.Domain.Response
{
    public class ProcedureResponse
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Cid { get; set; }
        public bool IsPrimary { get; set; }
    }
}