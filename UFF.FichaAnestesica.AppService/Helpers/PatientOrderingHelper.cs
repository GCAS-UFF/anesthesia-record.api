using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Service.Helpers
{
    public static class PatientOrderingHelper
    {
        public static List<Patient> Apply(
            List<Patient> patients,
            bool ascending = true,
            string orderBy = "surgerydate")
        {
            return orderBy?.ToLower() switch
            {
                "fullname" => ascending
                    ? patients.OrderBy(p => p.FullName).ToList()
                    : patients.OrderByDescending(p => p.FullName).ToList(),

                "medicalrecordnumber" => ascending
                    ? patients.OrderBy(p => p.MedicalRecordNumber).ToList()
                    : patients.OrderByDescending(p => p.MedicalRecordNumber).ToList(),

                "birthdate" => ascending
                    ? patients.OrderBy(p => p.BirthDate).ToList()
                    : patients.OrderByDescending(p => p.BirthDate).ToList(),

                "surgeriescount" => ascending
                    ? patients.OrderBy(p => p.Surgeries.Count).ToList()
                    : patients.OrderByDescending(p => p.Surgeries.Count).ToList(),

                "surgerydate" => ascending
                    ? patients.OrderBy(p =>
                        p.Surgeries.Any()
                            ? p.Surgeries.Min(s => s.SurgeryDate)
                            : DateTime.MaxValue)
                        .ToList()

                    : patients.OrderByDescending(p =>
                        p.Surgeries.Any()
                            ? p.Surgeries.Max(s => s.SurgeryDate)
                            : DateTime.MinValue)
                        .ToList(),

                _ => patients
                    .OrderBy(p => p.FullName)
                    .ToList()
            };
        }
    }
}
