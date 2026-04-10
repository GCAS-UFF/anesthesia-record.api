using UFF.FichaAnestesica.Domain.Dto;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class User : Base
    {
        // EntityFramework requires a parameterless constructor
        protected User() { }

        public User(UserDto userDto)
        {
            UserName = userDto.UserName;
            PassWord = userDto.PassWord;
        }

        public string UserName { get; private set; }
        public string PassWord { get; private set; }        
    }
}
