namespace UFF.FichaAnestesica.Domain.Dto
{
    public class UserResponse
    {
        public int Id { get; set; }
        public int ExternalId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Registration { get; set; }
    }
}
