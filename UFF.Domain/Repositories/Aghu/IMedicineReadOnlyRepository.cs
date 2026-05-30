using UFF.FichaAnestesica.Domain.Dto;

namespace UFF.FichaAnestesica.Domain.Repositories.ReadOnly
{
    public interface IMedicineReadOnlyRepository
    {
        Task<DrugListDto> GetDrugssFromAGHU();      
    }
}
