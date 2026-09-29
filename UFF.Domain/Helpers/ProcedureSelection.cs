using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Domain.Helpers
{
    public record ProcedureChoice(Procedure Procedure, bool IsPrimary, TimeOnly? Time = null);
    
    public static class ProcedureSelection
    {        
        public static bool HasChanged(IEnumerable<(string? Id, bool IsPrimary)> current, IEnumerable<(string? Id, bool IsPrimary)>? baseline)
        {
            if (baseline == null)
                return true;

            return Key(current) != Key(baseline);
        }

        private static string Key(IEnumerable<(string? Id, bool IsPrimary)> items)
        {
            var valid = items
                .Where(x => !string.IsNullOrWhiteSpace(x.Id))
                .Select(x => (Id: x.Id!.Trim(), x.IsPrimary))
                .ToList();

            var ids = valid.Select(x => x.Id).Distinct().OrderBy(x => x, StringComparer.Ordinal);
            var primary = valid.FirstOrDefault(x => x.IsPrimary).Id ?? valid.FirstOrDefault().Id ?? string.Empty;

            return $"{string.Join(",", ids)}|{primary}";
        }
    }
}
