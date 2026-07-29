using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Response;

namespace UFF.FichaAnestesica.Service.Mappers
{
    public static class ProcedureResponseMapper
    {
        public static List<ProcedureResponse> Map(List<Procedure> procedures)
        {
            if (procedures == null)
                return null;

            return procedures.Select(procedure => new ProcedureResponse
            {
                Description = procedure.Description,
                Cid = procedure.Cid,                
                Id = procedure.ExternalId         
            }).ToList();
        }
    }
}