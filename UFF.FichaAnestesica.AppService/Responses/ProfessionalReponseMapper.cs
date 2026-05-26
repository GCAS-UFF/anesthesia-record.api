using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Response;

namespace UFF.FichaAnestesica.Service.Mappers
{
    public static class ProfessionalReponseMapper
    {
        public static UserResponse Map(UserDto userDto)
        {
            if (userDto == null)
                return null;

            return new UserResponse
            {
                Email = userDto.Email,
                Id = userDto.Id,
                Login = userDto.Login,
                Name = userDto.Name,
                Registration = userDto.Registration
            };
        }

        public static List<UserResponse> Map(IEnumerable<UserDto> professionals)
            => professionals.Select(Map).ToList();
    }
}