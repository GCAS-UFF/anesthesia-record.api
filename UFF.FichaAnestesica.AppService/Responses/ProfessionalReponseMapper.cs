using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Service.Mappers
{
    public static class ProfessionalReponseMapper
    {
        public static List<UserResponse> Map(List<User> users)
        {
            if (users == null)
                return null;

            return users.Select(user => new UserResponse
            {
                Email = user.Email,
                Id = user.Id,
                Login = user.Login,
                Name = user.Name,
                ExternalId = user.ExternalId,
                Registration = user.Registration,
            }).ToList();
        }
    }
}