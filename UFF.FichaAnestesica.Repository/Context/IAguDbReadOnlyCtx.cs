using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.Domain.Dto;

namespace UFF.FichaAnestesica.Infra.Context
{
    public interface IAguDbReadOnlyCtx
    {
        public DbSet<PatientViewDto> PatientViews { get; set; }   
    }
}
