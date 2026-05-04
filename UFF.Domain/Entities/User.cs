using UFF.FichaAnestesica.Domain.Dto;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class User : Base
    {
        private User() { }

        public User(PatientViewDto userDto)
        {         
        }

        public string UserName { get; private set; }
        public string PassWord { get; private set; }        
    }
}
