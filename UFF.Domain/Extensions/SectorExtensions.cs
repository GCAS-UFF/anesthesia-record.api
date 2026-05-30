using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Extensions
{
    public static class SectorExtensions
    {
        public static SectorEnum ParseToEnum(string sector)
        {
            if (string.IsNullOrWhiteSpace(sector))
                return SectorEnum.Other;

            sector = sector.Trim().ToLower();

            return sector switch
            {
                "centro cirurgico" => SectorEnum.SurgicalCenter,
                "centro cirúrgico" => SectorEnum.SurgicalCenter,
                "centro obstetrico" => SectorEnum.ObstetricCenter,
                "centro obstétrico" => SectorEnum.ObstetricCenter,
                "rpa" => SectorEnum.PostAnesthesiaCareUnit,
                "recuperacao pos-anestesica" => SectorEnum.PostAnesthesiaCareUnit,
                "recuperação pós-anestésica" => SectorEnum.PostAnesthesiaCareUnit,
                "uti adulto" => SectorEnum.AdultICU,
                "uti" => SectorEnum.AdultICU,
                "uti pediatrica" => SectorEnum.PediatricICU,
                "uti pediátrica" => SectorEnum.PediatricICU,
                "uti neonatal" => SectorEnum.NeonatalICU,
                "emergencia" => SectorEnum.Emergency,
                "emergência" => SectorEnum.Emergency,
                "pronto atendimento" => SectorEnum.EmergencyCare,
                "enfermaria" => SectorEnum.Ward,
                "ambulatorio" => SectorEnum.OutpatientClinic,
                "ambulatório" => SectorEnum.OutpatientClinic,
                "hemodinamica" => SectorEnum.Hemodynamics,
                "hemodinâmica" => SectorEnum.Hemodynamics,
                "endoscopia" => SectorEnum.Endoscopy,
                "radiologia" => SectorEnum.Radiology,
                "tomografia" => SectorEnum.Tomography,
                "ressonancia magnetica" => SectorEnum.MagneticResonanceImaging,
                "ressonância magnética" => SectorEnum.MagneticResonanceImaging,
                "laboratorio" => SectorEnum.Laboratory,
                "laboratório" => SectorEnum.Laboratory,
                "banco de sangue" => SectorEnum.BloodBank,
                "central de material e esterilizacao" => SectorEnum.SterileProcessingDepartment,
                "central de material e esterilização" => SectorEnum.SterileProcessingDepartment,
                "pediatria" => SectorEnum.Pediatrics,
                "maternidade" => SectorEnum.Maternity,

                _ => SectorEnum.Other
            };
        }
    }
}