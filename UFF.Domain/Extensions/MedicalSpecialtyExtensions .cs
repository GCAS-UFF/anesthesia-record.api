using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Extensions
{
    public static class MedicalSpecialtyExtensions
    {
        public static MedicalSpecialtyEnum ParseToEnum(string specialty)
        {
            if (string.IsNullOrWhiteSpace(specialty))
                return MedicalSpecialtyEnum.Anesthesiology;

            specialty = specialty.Trim().ToLower();

            return specialty switch
            {
                "anestesiologia" => MedicalSpecialtyEnum.Anesthesiology,
                "anesthesiology" => MedicalSpecialtyEnum.Anesthesiology,

                "cardiologia" => MedicalSpecialtyEnum.Cardiology,
                "cardiology" => MedicalSpecialtyEnum.Cardiology,

                "cirurgia geral" => MedicalSpecialtyEnum.GeneralSurgery,
                "general surgery" => MedicalSpecialtyEnum.GeneralSurgery,

                "cirurgia plástica" => MedicalSpecialtyEnum.PlasticSurgery,
                "cirurgia plastica" => MedicalSpecialtyEnum.PlasticSurgery,
                "plastic surgery" => MedicalSpecialtyEnum.PlasticSurgery,

                "cirurgia torácica" => MedicalSpecialtyEnum.ThoracicSurgery,
                "cirurgia toracica" => MedicalSpecialtyEnum.ThoracicSurgery,
                "thoracic surgery" => MedicalSpecialtyEnum.ThoracicSurgery,

                "cirurgia vascular" => MedicalSpecialtyEnum.VascularSurgery,
                "vascular surgery" => MedicalSpecialtyEnum.VascularSurgery,

                "clínica médica" => MedicalSpecialtyEnum.InternalMedicine,
                "clinica medica" => MedicalSpecialtyEnum.InternalMedicine,
                "medicina interna" => MedicalSpecialtyEnum.InternalMedicine,
                "internal medicine" => MedicalSpecialtyEnum.InternalMedicine,

                "dermatologia" => MedicalSpecialtyEnum.Dermatology,
                "dermatology" => MedicalSpecialtyEnum.Dermatology,

                "endocrinologia" => MedicalSpecialtyEnum.Endocrinology,
                "endocrinology" => MedicalSpecialtyEnum.Endocrinology,

                "gastroenterologia" => MedicalSpecialtyEnum.Gastroenterology,
                "gastroenterology" => MedicalSpecialtyEnum.Gastroenterology,

                "ginecologia" => MedicalSpecialtyEnum.Gynecology,
                "gynecology" => MedicalSpecialtyEnum.Gynecology,

                "hematologia" => MedicalSpecialtyEnum.Hematology,
                "hematology" => MedicalSpecialtyEnum.Hematology,

                "infectologia" => MedicalSpecialtyEnum.InfectiousDisease,
                "infectious disease" => MedicalSpecialtyEnum.InfectiousDisease,

                "nefrologia" => MedicalSpecialtyEnum.Nephrology,
                "nephrology" => MedicalSpecialtyEnum.Nephrology,

                "neurologia" => MedicalSpecialtyEnum.Neurology,
                "neurology" => MedicalSpecialtyEnum.Neurology,

                "neurocirurgia" => MedicalSpecialtyEnum.Neurosurgery,
                "neurosurgery" => MedicalSpecialtyEnum.Neurosurgery,

                "oftalmologia" => MedicalSpecialtyEnum.Ophthalmology,
                "ophthalmology" => MedicalSpecialtyEnum.Ophthalmology,

                "oncologia" => MedicalSpecialtyEnum.Oncology,
                "oncology" => MedicalSpecialtyEnum.Oncology,

                "ortopedia" => MedicalSpecialtyEnum.Orthopedics,
                "orthopedics" => MedicalSpecialtyEnum.Orthopedics,
                "orthopaedics" => MedicalSpecialtyEnum.Orthopedics,

                "otorrinolaringologia" => MedicalSpecialtyEnum.Otorhinolaryngology,
                "ent" => MedicalSpecialtyEnum.Otorhinolaryngology,
                "otorhinolaryngology" => MedicalSpecialtyEnum.Otorhinolaryngology,

                "pediatria" => MedicalSpecialtyEnum.Pediatrics,
                "pediatrics" => MedicalSpecialtyEnum.Pediatrics,

                "pneumologia" => MedicalSpecialtyEnum.Pulmonology,
                "pulmonology" => MedicalSpecialtyEnum.Pulmonology,

                "psiquiatria" => MedicalSpecialtyEnum.Psychiatry,
                "psychiatry" => MedicalSpecialtyEnum.Psychiatry,

                "radiologia" => MedicalSpecialtyEnum.Radiology,
                "radiology" => MedicalSpecialtyEnum.Radiology,

                "reumatologia" => MedicalSpecialtyEnum.Rheumatology,
                "rheumatology" => MedicalSpecialtyEnum.Rheumatology,

                "urologia" => MedicalSpecialtyEnum.Urology,
                "urology" => MedicalSpecialtyEnum.Urology,

                _ => MedicalSpecialtyEnum.Anesthesiology
            };
        }
    }
}