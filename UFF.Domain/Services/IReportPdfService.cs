using UFF.FichaAnestesica.Domain.Commands.Reports;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Services
{
    public interface IReportPdfService
    {
        Task<(byte[]? Bytes, string? Error)> GenerateAsync(string reportKey, ReportFilterQuery filter, DrugCategoryEnum? category);
    }
}
