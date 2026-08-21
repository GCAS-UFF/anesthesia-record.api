using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class User : Base
    {
        protected User() { }

        public int ExternalId { get; protected set; }
        public string Name { get; protected set; }
        public string Registration { get; protected set; }
        public SectorEnum? Sector { get; protected set; }
        public bool IsAdmin { get; protected set; }
        public string Password {get; protected set;}
        public string Login { get; protected set; }
        public string Email { get; protected set; }
        public bool CanLogIn { get; protected set; }
        public UserStatusEnum Status { get; protected set; }
        public MedicalSpecialtyEnum MedicalSpecialty { get; protected set; }
        public DateTime? LastLoginAt { get; protected set; }
        public DateTime? LastSyncAt { get; protected set; }

        public static User Create(int externalId, string name, string email, string login, string registration, MedicalSpecialtyEnum medicalSpecialty, SectorEnum sector)
        {
            return new User
            {
                ExternalId = externalId,
                Name = name,
                Email = email,
                Login = login,
                MedicalSpecialty = medicalSpecialty,
                Sector = sector,
                CanLogIn = true,
                Registration = registration,
                Status = UserStatusEnum.Enabled,
                CreatedAt = DateTime.UtcNow,
                LastLoginAt = DateTime.UtcNow
            };
        }

        public void Update(string name, string email, string login, string registration, MedicalSpecialtyEnum medicalSpecialty, SectorEnum sector)
        {
            Name = string.IsNullOrWhiteSpace(name) ? Name : name;
            Email = string.IsNullOrWhiteSpace(email) ? Email : email;
            Login = string.IsNullOrWhiteSpace(login) ? Login : login;
            Registration = string.IsNullOrWhiteSpace(registration) ? Registration : registration;
            Status = UserStatusEnum.Enabled;
            MedicalSpecialty = medicalSpecialty;
            Sector = sector;
            LastSyncAt = DateTime.UtcNow;
        }

        public void Disable()
        {
            Status = UserStatusEnum.Disabled;
            LastSyncAt = DateTime.UtcNow;
        }

        public void RegisterLogin()
        {
            LastLoginAt = DateTime.UtcNow;
        }
    }
}