using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Extensions;

namespace UFF.FichaAnestesica.Domain.Response
{
    public class AnesthesiaRecordResponse
    {
        public PatientSurgeryResponse Patient { get; set; }
        public int SurgeryId { get; set; }

        #region Pré-medicação
        public int? PreAnestheticMedicationId { get; set; }
        public string? PreAnestheticMedicationName { get; set; }
        public string? PreAnestheticMedicationDose { get; set; }
        public string? PreAnestheticMedicationRoute { get; set; }
        public string? PreAnestheticMedicationOtherRoute { get; set; }
        public TimeOnly? PreAnestheticMedicationTime { get; set; }
        #endregion

        public List<SurgeryResponse> Surgeries { get; set; }
        public List<AntibioticResponse>? AntibioticsList { get; set; }
        public string? SurgeonRegistration { get; set; }
        public string? AssistantRegistration { get; set; }
        public bool ProceduresCustomized { get; set; }

        #region Dor
        public bool? DorUsouENV { get; set; }
        public int? DorENV { get; set; }
        public bool? DorUsouPAINAD { get; set; }
        public int? DorPAINAD { get; set; }
        public bool? DorUsouBPS { get; set; }
        public int? DorBPS { get; set; }
        public string? Conduta { get; set; }
        #endregion

        #region Segurança
        public bool? PatientIdentifiedBeforeInduction { get; set; }
        public bool? AnestheticConsentSigned { get; set; }
        public bool? AnesthesiaEquipmentChecked { get; set; }
        public string? SafetyObservations { get; set; }
        public bool? PreAnestheticMedication { get; set; }
        #endregion

        #region Antibióticos
        public bool? ProphylacticAntibioticUsed { get; set; }
        #endregion

        #region Sinais Vitais
        public string? BloodPressure { get; set; }
        public int? RespiratoryRate { get; set; }
        public decimal? Temperature { get; set; }
        public int? OxygenSaturation { get; set; }
        public decimal? WeightKg { get; set; }
        public AsaClassificationEnum? AsaClassification { get; set; }
        #endregion

        #region Horários
        public TimeOnly? RoomEntryTime { get; set; }
        public TimeOnly? AnesthesiaStartTime { get; set; }
        public TimeOnly? SurgeryEndTime { get; set; }
        public TimeOnly? AnesthesiaEndTime { get; set; }
        #endregion

        #region Procedimento
        public string? PreOperativeDiagnosis { get; set; }
        public SurgicalPositionEnum? SurgicalPosition { get; set; }
        public string? OtherSurgicalPosition { get; set; }
        public bool? UsesCushions { get; set; }
        public string? CushionsAccessLocation { get; set; }
        public VenousAccessTypeEnum? VenousAccessType { get; set; }
        public string? OtherVenousAccess { get; set; }
        public string? VenousAccessLocation { get; set; }
        public bool? DifficultVenousPuncture { get; set; }
        public bool? GeneralAnesthesia { get; set; }
        public RespirationModeEnum? RespirationMode { get; set; }
        public ControlledVentilationModeEnum? ControlledVentilationMode { get; set; }
        public bool? Co2AbsorberCircuit { get; set; }
        #endregion

        #region Via Aérea - Dispositivos
        public List<AirwayDeviceResponse> AirwayDevices { get; set; } = new();
        public string? AirwayDeviceNumbers { get; set; }
        public bool? Cuff { get; set; }
        public bool? Iot { get; set; }
        public bool? OralTube { get; set; }
        public bool? NasalTube { get; set; }
        public IntubationDifficultyEnum? IntubationDifficulty { get; set; }
        #endregion

        #region Via Aérea - Tipo
        public AirwayTypeEnum? AirwayType { get; set; }
        public string? OtherAirwayTypeDescription { get; set; }
        #endregion

        #region Via Aérea - Técnicas
        public bool? Laryngoscopy { get; set; }
        public bool? RetrogradeTechnique { get; set; }
        public bool? VideoLaryngoscopy { get; set; }
        public bool? Bronchofibroscopy { get; set; }
        public bool? Tracheostomy { get; set; }
        public bool? HasOtherAirwayTechnique { get; set; }
        public string? OtherAirwayTechnique { get; set; }
        #endregion

        #region Bloqueios Espinhais
        public bool? SpinalBlockPerformed { get; set; }
        public List<PunctureLevelResponse> PunctureLevels { get; set; } = new();
        public PuncturePositionEnum? PuncturePosition { get; set; }
        public bool? SpinalCatheter { get; set; }
        public bool? SpinalOpioid { get; set; }
        public int? PunctureCount { get; set; }
        #endregion

        #region Sedação e Oxigênio
        public bool? SedationPerformed { get; set; }
        public bool? OxygenSupplementation { get; set; }
        public List<OxygenSupplementationResponse> OxygenSupplementationTypes { get; set; } = new();
        public bool? HasOxygenSupplementationOther { get; set; }
        public string? OxygenSupplementationOther { get; set; }
        #endregion

        #region Bloqueio Plexo
        public bool? PlexusBlockPerformed { get; set; }
        public bool? NeurostimulatorUsed { get; set; }
        public List<StimulatedNerveResponse> StimulatedNerves { get; set; } = new();
        #endregion

        public string? SurgeryPerformed { get; set; }
        public string? PostOperativeDiagnosis { get; set; }

        #region Recuperação
        public int? ConsciousnessScore { get; set; }
        public int? ActivityScore { get; set; }
        public int? CirculationScore { get; set; }
        public int? RespirationScore { get; set; }
        public int? OxygenSaturationScore { get; set; }
        public int? TotalAldreteKroulikScore { get; set; }
        public TimeOnly? AldreteEvaluationTime { get; set; }
        public ClinicalDischargeConditionEnum? ClinicalDischargeCondition { get; set; }
        public string? DischargeConditionOther { get; set; }
        public PatientDestinationEnum? Destination { get; set; }
        public bool? HasPain { get; set; }
        #endregion

        #region Assinatura
        public DateTime? SignatureDate { get; set; }
        #endregion

        public int? FirstAnesthesiologistId { get; set; }
        public string? FirstAnesthesiologistName { get; set; }
        public int? SecondAnesthesiologistId { get; set; }
        public string? SecondAnesthesiologistName { get; set; }
        public int? SurgeonId { get; set; }
        public string? SurgeonName { get; set; }
        public int? AssistantId { get; set; }
        public string? AssistantName { get; set; }
        public string ExternalPatientId { get; set; } = string.Empty;
        public DateOnly RecordDate { get; set; }
        public DateTime SurgeryDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdate { get; set; }
        public SurgeryStatusEnum Status { get; set; }

        public static AnesthesiaRecordResponse ToResponse(AnesthesiaRecord anesthesiaRecord, PatientDetailDto patientDetail)
        {
            var surgeries = anesthesiaRecord.ProceduresCustomized && anesthesiaRecord.Surgeries.Any() ?
             BuildProceduresFromRecord(anesthesiaRecord) : patientDetail.Surgeries?.Select(MapSurgery).ToList() ?? [];

            return new AnesthesiaRecordResponse
            {
                Patient = new PatientSurgeryResponse()
                {
                    FullName = patientDetail.FullName,
                    BirthDate = patientDetail.BirthDate,
                    Age = patientDetail.BirthDate.Date.GetAge(),
                    Status = patientDetail.HaveFirstAnesthesist && ParseStatus(patientDetail.Status) != SurgeryStatusEnum.Completed ? SurgeryStatusEnum.InProgress : ParseStatus(patientDetail.Status),
                    Gender = patientDetail.Gender,
                    WeightKg = patientDetail.WeightKg,
                    MedicalRecordNumber = patientDetail.MedicalRecordNumber,
                    Allergies = patientDetail.Allergies?.Select(MapAllergy).ToList() ?? new List<ListAllergyDto>(),
                    CurrentLocation = MapLocation(patientDetail.CurrentLocation)
                },
                SurgeryId = anesthesiaRecord.Id,
                ExternalPatientId = anesthesiaRecord.PatientId,
                Surgeries = surgeries,

                // Pré-medicação
                PreAnestheticMedicationId = anesthesiaRecord.PreAnestheticMedicationId,
                PreAnestheticMedicationName = anesthesiaRecord.PreAnestheticMedicationName,
                PreAnestheticMedicationDose = anesthesiaRecord.PreAnestheticMedicationDose,
                PreAnestheticMedicationRoute = anesthesiaRecord.PreAnestheticMedicationRoute,
                PreAnestheticMedicationOtherRoute = anesthesiaRecord.PreAnestheticMedicationOtherRoute,
                PreAnestheticMedicationTime = anesthesiaRecord.PreAnestheticMedicationTime,

                // Antibióticos
                AntibioticsList = anesthesiaRecord.Antibiotics?.Select(MapAntibiotic).ToList() ?? new List<AntibioticResponse>(),

                // Equipe
                SurgeonRegistration = anesthesiaRecord.Surgeon?.Registration,
                AssistantRegistration = anesthesiaRecord.Assistant?.Registration,

                ProceduresCustomized = anesthesiaRecord.ProceduresCustomized,

                // Dor
                DorUsouENV = anesthesiaRecord.DorUsouENV,
                DorENV = anesthesiaRecord.DorENV,
                DorUsouPAINAD = anesthesiaRecord.DorUsouPAINAD,
                DorPAINAD = anesthesiaRecord.DorPAINAD,
                DorUsouBPS = anesthesiaRecord.DorUsouBPS,
                DorBPS = anesthesiaRecord.DorBPS,
                Conduta = anesthesiaRecord.Conduta,

                // Segurança
                PatientIdentifiedBeforeInduction = anesthesiaRecord.PatientIdentifiedBeforeInduction,
                AnestheticConsentSigned = anesthesiaRecord.AnestheticConsentSigned,
                AnesthesiaEquipmentChecked = anesthesiaRecord.AnesthesiaEquipmentChecked,
                SafetyObservations = anesthesiaRecord.SafetyObservations,
                PreAnestheticMedication = anesthesiaRecord.PreAnestheticMedication,
                ProphylacticAntibioticUsed = anesthesiaRecord.ProphylacticAntibioticUsed,

                // Sinais Vitais
                BloodPressure = anesthesiaRecord.BloodPressure,
                RespiratoryRate = anesthesiaRecord.RespiratoryRate,
                Temperature = anesthesiaRecord.Temperature,
                OxygenSaturation = anesthesiaRecord.OxygenSaturation,
                WeightKg = anesthesiaRecord.WeightKg,
                AsaClassification = anesthesiaRecord.AsaClassification,

                // Horários
                RoomEntryTime = anesthesiaRecord.RoomEntryTime,
                AnesthesiaStartTime = anesthesiaRecord.AnesthesiaStartTime,
                SurgeryEndTime = anesthesiaRecord.SurgeryEndTime,
                AnesthesiaEndTime = anesthesiaRecord.AnesthesiaEndTime,

                // Procedimento
                PreOperativeDiagnosis = anesthesiaRecord.PreOperativeDiagnosis,
                SurgicalPosition = anesthesiaRecord.SurgicalPosition,
                OtherSurgicalPosition = anesthesiaRecord.OtherSurgicalPosition,
                UsesCushions = anesthesiaRecord.UsesCushions,
                CushionsAccessLocation = anesthesiaRecord.CushionsAccessLocation,
                VenousAccessType = anesthesiaRecord.VenousAccessType,
                OtherVenousAccess = anesthesiaRecord.OtherVenousAccess,
                VenousAccessLocation = anesthesiaRecord.VenousAccessLocation,
                DifficultVenousPuncture = anesthesiaRecord.DifficultVenousPuncture,
                GeneralAnesthesia = anesthesiaRecord.GeneralAnesthesia,
                RespirationMode = anesthesiaRecord.RespirationMode,
                ControlledVentilationMode = anesthesiaRecord.ControlledVentilationMode,
                Co2AbsorberCircuit = anesthesiaRecord.Co2AbsorberCircuit,

                // Via Aérea - Dispositivos
                AirwayDevices = anesthesiaRecord.AirwayDevices?.Select(MapAirwayDevice).ToList() ?? new List<AirwayDeviceResponse>(),
                AirwayDeviceNumbers = anesthesiaRecord.AirwayDeviceNumbers,
                Cuff = anesthesiaRecord.Cuff,
                Iot = anesthesiaRecord.Iot,
                OralTube = anesthesiaRecord.OralTube,
                NasalTube = anesthesiaRecord.NasalTube,
                IntubationDifficulty = anesthesiaRecord.IntubationDifficulty,

                // Via Aérea - Tipo
                AirwayType = anesthesiaRecord.AirwayType,
                OtherAirwayTypeDescription = anesthesiaRecord.OtherAirwayTypeDescription,

                // Via Aérea - Técnicas
                Laryngoscopy = anesthesiaRecord.Laryngoscopy,
                RetrogradeTechnique = anesthesiaRecord.RetrogradeTechnique,
                VideoLaryngoscopy = anesthesiaRecord.VideoLaryngoscopy,
                Bronchofibroscopy = anesthesiaRecord.Bronchofibroscopy,
                Tracheostomy = anesthesiaRecord.Tracheostomy,
                HasOtherAirwayTechnique = anesthesiaRecord.HasOtherAirwayTechnique,
                OtherAirwayTechnique = anesthesiaRecord.OtherAirwayTechnique,

                // Bloqueios Espinhais
                SpinalBlockPerformed = anesthesiaRecord.SpinalBlockPerformed,
                PunctureLevels = anesthesiaRecord.PunctureLevels?.Select(MapPunctureLevel).ToList() ?? new List<PunctureLevelResponse>(),
                PuncturePosition = anesthesiaRecord.PuncturePosition,
                SpinalCatheter = anesthesiaRecord.SpinalCatheter,
                SpinalOpioid = anesthesiaRecord.SpinalOpioid,
                PunctureCount = anesthesiaRecord.PunctureCount,

                // Sedação e Oxigênio
                SedationPerformed = anesthesiaRecord.SedationPerformed,
                OxygenSupplementation = anesthesiaRecord.OxygenSupplementation,
                OxygenSupplementationTypes = anesthesiaRecord.OxygenSupplementationTypes?.Select(MapOxygenSupplementation).ToList() ?? new List<OxygenSupplementationResponse>(),
                HasOxygenSupplementationOther = anesthesiaRecord.HasOxygenSupplementationOther,
                OxygenSupplementationOther = anesthesiaRecord.OxygenSupplementationOther,

                // Bloqueio Plexo
                PlexusBlockPerformed = anesthesiaRecord.PlexusBlockPerformed,
                NeurostimulatorUsed = anesthesiaRecord.NeurostimulatorUsed,
                StimulatedNerves = anesthesiaRecord.StimulatedNerves?.Select(MapStimulatedNerve).ToList() ?? new List<StimulatedNerveResponse>(),

                SurgeryPerformed = anesthesiaRecord.SurgeryPerformed,
                PostOperativeDiagnosis = anesthesiaRecord.PostOperativeDiagnosis,

                // Recuperação
                ConsciousnessScore = anesthesiaRecord.ConsciousnessScore,
                ActivityScore = anesthesiaRecord.ActivityScore,
                CirculationScore = anesthesiaRecord.CirculationScore,
                RespirationScore = anesthesiaRecord.RespirationScore,
                OxygenSaturationScore = anesthesiaRecord.OxygenSaturationScore,
                TotalAldreteKroulikScore = anesthesiaRecord.TotalAldreteKroulikScore,
                AldreteEvaluationTime = anesthesiaRecord.AldreteEvaluationTime,
                ClinicalDischargeCondition = anesthesiaRecord.ClinicalDischargeCondition,
                DischargeConditionOther = anesthesiaRecord.DischargeConditionOther,
                Destination = anesthesiaRecord.Destination,
                HasPain = anesthesiaRecord.HasPain,

                // Assinatura
                SignatureDate = anesthesiaRecord.SignatureDate,

                Status = anesthesiaRecord.Status,
                FirstAnesthesiologistId = anesthesiaRecord.FirstAnesthesiologistId,
                FirstAnesthesiologistName = anesthesiaRecord.FirstAnesthesiologist?.Name,
                SecondAnesthesiologistId = anesthesiaRecord.SecondAnesthesiologistId,
                SecondAnesthesiologistName = anesthesiaRecord.SecondAnesthesiologist?.Name,
                SurgeonId = anesthesiaRecord.SurgeonId,
                SurgeonName = anesthesiaRecord.Surgeon?.Name,
                AssistantId = anesthesiaRecord.AssistantId,
                AssistantName = anesthesiaRecord.Assistant?.Name,
                SurgeryDate = anesthesiaRecord.SurgeryDate,
                CreatedAt = anesthesiaRecord.CreatedAt,
                LastUpdate = anesthesiaRecord.LastUpdate,
            };
        }

        #region Métodos de Mapeamento

        private static AntibioticResponse MapAntibiotic(AnesthesiaRecordAntibiotic antibiotic)
        {
            return new AntibioticResponse
            {
                MedicationId = antibiotic.MedicationId,
                MedicationName = antibiotic.MedicationName,
                Name = antibiotic.MedicationName,
                Dose = antibiotic.Dose,
                Route = antibiotic.Route,
                Time = antibiotic.Time,
                HasBooster = antibiotic.HasBooster,
                Boosters = antibiotic.Boosters?.Select(MapBooster).ToList() ?? new List<BoosterResponse>()
            };
        }

        private static BoosterResponse MapBooster(AnesthesiaRecordAntibioticBooster booster)
        {
            return new BoosterResponse
            {
                MedicationId = booster.MedicationId,
                MedicationName = booster.MedicationName,
                Name = booster.MedicationName,
                Dose = booster.Dose,
                Route = booster.Route,
                Time = booster.Time
            };
        }

        private static AirwayDeviceResponse MapAirwayDevice(AnesthesiaRecordAirwayDevice device)
        {
            return new AirwayDeviceResponse
            {
                DeviceType = device.DeviceType
            };
        }

        private static PunctureLevelResponse MapPunctureLevel(AnesthesiaRecordPunctureLevel level)
        {
            return new PunctureLevelResponse
            {
                Level = level.Level
            };
        }

        private static OxygenSupplementationResponse MapOxygenSupplementation(AnesthesiaRecordOxygenSupplementation type)
        {
            return new OxygenSupplementationResponse
            {
                Type = type.Type
            };
        }

        private static StimulatedNerveResponse MapStimulatedNerve(AnesthesiaRecordStimulatedNerve nerve)
        {
            return new StimulatedNerveResponse
            {
                Nerve = nerve.Nerve
            };
        }

        private static PatientLocationResponse? MapLocation(CurrentLocationDto? location)
        {
            if (location == null)
                return null;

            return new PatientLocationResponse
            {
                Unit = location.Unit == null
                    ? null
                    : new UnitResponse
                    {
                        Code = location.Unit.Code,
                        Description = location.Unit.Description
                    },
                Bed = location.Bed,
                Floor = location.Floor,
                Room = location.Room
            };
        }

        private static List<SurgeryResponse> BuildProceduresFromRecord(AnesthesiaRecord record)
        {
            return new List<SurgeryResponse>
            {
                new SurgeryResponse
                {
                    Id = record.Id,
                    SurgeryDate = record.SurgeryDate,
                    Status = record.Status,
                    Procedures = record.Surgeries
                        .Select(p => new ProcedureResponse
                        {
                            Id = p.Procedure.ExternalId,
                            Description = p.Procedure?.Description,
                            Cid = p.Procedure?.Cid,
                            IsPrimary = p.IsPrimary,
                            Time = p.Time
                        })
                        .ToList()
                }
            };
        }

        private static SurgeryResponse MapSurgery(SurgeryDetailsDto surgery)
        {
            return new SurgeryResponse
            {
                Id = surgery.Id,
                SurgeryDate = surgery.SurgeryDate,
                Status = ParseStatus(surgery.SurgeryStatus),
                Specialty = surgery.Specialty == null
                    ? null
                    : new SpecialtyResponse
                    {
                        Code = surgery.Specialty.Id,
                        Description = surgery.Specialty.Description
                    },
                Location = surgery.Location == null
                    ? null
                    : new SurgeryLocationResponse
                    {
                        Room = surgery.Location.Room,
                        SurgicalCenter = surgery.Location.SurgicalCenter == null
                            ? null
                            : new SurgicalCenterResponse
                            {
                                Code = surgery.Location.SurgicalCenter.Id,
                                Description = surgery.Location.SurgicalCenter.Description
                            }
                    },
                Procedures = surgery.Procedures?
                    .Select(p => new ProcedureResponse
                    {
                        Id = p.ExternalId.ToString(),
                        Description = p.Description,
                        Cid = p.Cid,
                        IsPrimary = p.IsPrimary,
                        Time = p.Time
                    })
                    .ToList() ?? new List<ProcedureResponse>()
            };
        }

        private static ListAllergyDto MapAllergy(Dto.ListAllergyDto allergy)
        {
            return new ListAllergyDto
            {
                RegisterDate = allergy.RegisterDate,
                Description = allergy.Description,
                Reason = allergy.Reason,
                AllergyCriticality = allergy.AllergyCriticality,
                CertaintyLevel = allergy.CertaintyLevel,
                CausativeAgent = allergy.CausativeAgent,
                Medication = allergy.Medication == null
                    ? null
                    : new MedicationResponse
                    {
                        Description = allergy.Medication.Description
                    }
            };
        }

        private static SurgeryStatusEnum ParseStatus(string status)
        {
            return status?.ToLower() switch
            {
                "agendada" => SurgeryStatusEnum.Scheduled,
                "em_progresso" => SurgeryStatusEnum.InProgress,
                "cancelada" => SurgeryStatusEnum.Canceled,
                "em_preparacao" => SurgeryStatusEnum.Preparing,
                "em_andamento" => SurgeryStatusEnum.InProgress,
                "finalizada" => SurgeryStatusEnum.Completed,
                _ => SurgeryStatusEnum.Scheduled
            };
        }

        #endregion
    }
}