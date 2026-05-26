using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class User : Base
    {
        protected User() { }

        public string ExternalId { get; protected set; }
        public string Name { get; protected set; }
        public string Registration { get; protected set; }
        public string? Sector { get; protected set; }
        public string Login { get; protected set; }
        public string Email { get; protected set; }
        public bool CanLogIn { get; protected set; }
        public UserStatusEnum Status { get; protected set; }

        public static User Create(string externalId, string name, string email, string login, string registration)
        {
            return new User
            {
                ExternalId = externalId,
                Name = name,
                Email = email,
                Login = login,
                Registration = registration,
                Status = UserStatusEnum.Enabled,
                CreatedAt = DateTime.UtcNow,
                LastLoginAt = DateTime.UtcNow
            };
        }
        public static User? Create(string name, object email, string login)
        {
            throw new NotImplementedException();
        }
    }
}