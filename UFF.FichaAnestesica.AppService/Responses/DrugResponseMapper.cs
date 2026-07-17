using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Response;

namespace UFF.FichaAnestesica.Service.Mappers
{
    public static class DrugResponseMapper
    {
        public static List<MedicationResponse> Map(List<Drug> drugs)
        {
            if (drugs == null)
                return null;

            return drugs.Select(drug => new MedicationResponse
            {
                Description = drug.Description,
                Id = drug.Id
            }).ToList();
        }
    }
}