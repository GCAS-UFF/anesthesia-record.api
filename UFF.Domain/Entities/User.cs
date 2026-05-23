using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class User : Base
    {
        protected User() { }
        public string Name { get; protected set; }
        public string Registration {  get; protected set; }
        public string Sector { get; protected set; }
        public string Email { get; protected set; }        
        public string Role { get; protected set; }
        public bool CanLogIn { get; protected set; }
        public UserStatusEnum Status { get; protected set; }       
    }
}
