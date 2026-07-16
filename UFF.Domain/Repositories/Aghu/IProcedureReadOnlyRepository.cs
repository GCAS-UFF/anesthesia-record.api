using UFF.FichaAnestesica.Domain.Dto;

namespace UFF.FichaAnestesica.Domain.Repositories.ReadOnly
{
    public interface IProcedureReadOnlyRepository
    {
        Task<ProcedureListDto> GetProceduresFromAGHU();      
    }
}
