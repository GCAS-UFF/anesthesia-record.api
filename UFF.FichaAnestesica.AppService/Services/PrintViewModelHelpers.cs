using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Response.Print;

namespace UFF.FichaAnestesica.Infra.Services
{
    internal static class PrintViewModelHelpers
    {
        public static PrintHospitalInfo BuildHospitalInfo(InstitutionSettings? institution)
        {
            if (institution == null)
            {
                return new PrintHospitalInfo
                {
                    Name = InstitutionSettings.DefaultHospitalName,
                    Sector = InstitutionSettings.DefaultHospitalSector
                };
            }

            var addressParts = new List<string>();

            if (!string.IsNullOrWhiteSpace(institution.HospitalStreet))
            {
                addressParts.Add(string.IsNullOrWhiteSpace(institution.HospitalNumber)
                    ? institution.HospitalStreet
                    : $"{institution.HospitalStreet}, {institution.HospitalNumber}");
            }

            if (!string.IsNullOrWhiteSpace(institution.HospitalNeighborhood))
                addressParts.Add(institution.HospitalNeighborhood);

            if (!string.IsNullOrWhiteSpace(institution.HospitalCity))
                addressParts.Add($"{institution.HospitalCity}/{institution.HospitalState}");

            return new PrintHospitalInfo
            {
                Name = institution.HospitalName,
                Sector = institution.HospitalSector,
                Cnpj = institution.HospitalCnpj,
                Address = addressParts.Count > 0 ? string.Join(" - ", addressParts) : null
            };
        }
    }
}
