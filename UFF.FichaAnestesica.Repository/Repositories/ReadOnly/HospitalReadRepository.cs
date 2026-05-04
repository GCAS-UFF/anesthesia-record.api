using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Extensions;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Domain.Response;
using UFF.FichaAnestesica.Infra.Context;

namespace UFF.FichaAnestesica.Infra.Repositories.ReadOnly
{
    public class HospitalReadRepository : IHospitalReadRepository
    {
        private readonly ISigaDbReadOnlyCtx _context;

        public HospitalReadRepository(ISigaDbReadOnlyCtx context)
        {
            _context = context;
        }

        public async Task<PagedResponse<PatientViewDto>> GetSurgeriesFromHospitalAsync(DateTime? date, SurgeryStatus? status, int page = 1, int pageSize = 10)
        {
            var query = _context.PatientViews.AsQueryable();

            if (date.HasValue)
                query = query.Where(x => x.SurgeryDate.Date == date.Value.Date);


            if (status.HasValue)
            {
                var statusString = status.Value.ToStatusString();
                query = query.Where(x => x.SurgeryStatus == statusString);
            }

            var totalCount = await query.GroupBy(x => x.FullName).CountAsync();

            var result = await query
                .OrderBy(x => x.SurgeryDate)
                .ThenBy(x => x.FullName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (result.Any())            
                result.ForEach(x => x.SurgeryStatus = x.SurgeryStatus.ToSurgeryStatus().ToString());           

            return new PagedResponse<PatientViewDto>
            {
                Data = result,
                Page = page,
                PageSize = pageSize,
                TotalItems = totalCount
            };
        }
    }
}