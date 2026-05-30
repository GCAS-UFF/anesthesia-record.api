using System.ComponentModel;

namespace UFF.FichaAnestesica.Domain.Enums
{
    public enum MedicalSpecialtyEnum
    {
        [Description("Anestesiologia")]
        Anesthesiology = 1,

        [Description("Cardiologia")]
        Cardiology = 2,

        [Description("Cirurgia Geral")]
        GeneralSurgery = 3,

        [Description("Cirurgia Plástica")]
        PlasticSurgery = 4,

        [Description("Cirurgia Torácica")]
        ThoracicSurgery = 5,

        [Description("Cirurgia Vascular")]
        VascularSurgery = 6,

        [Description("Clínica Médica")]
        InternalMedicine = 7,

        [Description("Dermatologia")]
        Dermatology = 8,

        [Description("Endocrinologia")]
        Endocrinology = 9,

        [Description("Gastroenterologia")]
        Gastroenterology = 10,

        [Description("Ginecologia")]
        Gynecology = 11,

        [Description("Hematologia")]
        Hematology = 12,

        [Description("Infectologia")]
        InfectiousDisease = 13,

        [Description("Nefrologia")]
        Nephrology = 14,

        [Description("Neurologia")]
        Neurology = 15,

        [Description("Neurocirurgia")]
        Neurosurgery = 16,

        [Description("Oftalmologia")]
        Ophthalmology = 17,

        [Description("Oncologia")]
        Oncology = 18,

        [Description("Ortopedia")]
        Orthopedics = 19,

        [Description("Otorrinolaringologia")]
        Otorhinolaryngology = 20,

        [Description("Pediatria")]
        Pediatrics = 21,

        [Description("Pneumologia")]
        Pulmonology = 22,

        [Description("Psiquiatria")]
        Psychiatry = 23,

        [Description("Radiologia")]
        Radiology = 24,

        [Description("Reumatologia")]
        Rheumatology = 25,

        [Description("Urologia")]
        Urology = 26
    }
}