using UFF.FichaAnestesica.Domain.Dto;

namespace UFF.FichaAnestesica.Domain.Repositories.ReadOnly
{
    public interface IMedicineApiReadOnlyRepository
    {
        Task<List<DrugDto>> GetDrugssFromAGHU();      
    }
}
