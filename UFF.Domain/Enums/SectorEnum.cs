using System.ComponentModel;

namespace UFF.FichaAnestesica.Domain.Enums
{
    public enum SectorEnum
    {
        [Description("Centro Cirúrgico")]
        SurgicalCenter = 1,

        [Description("Centro Obstétrico")]
        ObstetricCenter = 2,

        [Description("Recuperação Pós-Anestésica")]
        PostAnesthesiaCareUnit = 3,

        [Description("UTI Adulto")]
        AdultICU = 4,

        [Description("UTI Pediátrica")]
        PediatricICU = 5,

        [Description("UTI Neonatal")]
        NeonatalICU = 6,

        [Description("Emergência")]
        Emergency = 7,

        [Description("Pronto Atendimento")]
        EmergencyCare = 8,

        [Description("Enfermaria")]
        Ward = 9,

        [Description("Ambulatório")]
        OutpatientClinic = 10,

        [Description("Hemodinâmica")]
        Hemodynamics = 11,

        [Description("Endoscopia")]
        Endoscopy = 12,

        [Description("Radiologia")]
        Radiology = 13,

        [Description("Tomografia")]
        Tomography = 14,

        [Description("Ressonância Magnética")]
        MagneticResonanceImaging = 15,

        [Description("Laboratório")]
        Laboratory = 16,

        [Description("Banco de Sangue")]
        BloodBank = 17,

        [Description("Central de Material e Esterilização")]
        SterileProcessingDepartment = 18,

        [Description("Pediatria")]
        Pediatrics = 19,

        [Description("Maternidade")]
        Maternity = 20,

        [Description("Outro")]
        Other = 99
    }
}