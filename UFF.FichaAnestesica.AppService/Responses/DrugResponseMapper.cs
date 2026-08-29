using UFF.FichaAnestesica.CrossCutting.Extensions;
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

        public static DrugAdminResponse MapAdmin(Drug drug)
        {
            if (drug == null)
                return null;

            return new DrugAdminResponse
            {
                Id = drug.Id,
                Description = drug.Description,
                DefaultUnit = drug.DefaultUnit,
                Active = drug.Active,
                CategoryId = (int)drug.Category,
                CategoryLabel = EnumExtensions.GetDescription(drug.Category)
            };
        }

        public static List<DrugAdminResponse> MapAdmin(List<Drug> drugs)
        {
            if (drugs == null)
                return null;

            return drugs.Select(MapAdmin).ToList();
        }
    }
}