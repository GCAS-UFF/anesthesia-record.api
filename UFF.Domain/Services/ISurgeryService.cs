using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Services
{
    public interface ISurgeryService
    {
        Task<IEnumerable<SurgeryListDto>> GetSurgeriesAsync(DateTime date, SurgeryStatus status);
    }
}