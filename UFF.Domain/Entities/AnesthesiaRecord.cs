using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class AnesthesiaRecord
    {
        private AnesthesiaRecord() { }

        public int Id { get; private set; }
        public bool ProceduresCustomized { get; private set; }

        #region Segurança
        public bool? PatientIdentifiedBeforeInduction { get; private set; }
        public bool? AnestheticConsentSigned { get; private set; }
        public bool? AnesthesiaEquipmentChecked { get; private set; }
        public string? SafetyObservations { get; private set; }
        #endregion

        #region Pré-medicação
        public bool? PreAnestheticMedication { get; private set; }
        public int? PreAnestheticMedicationId { get; private set; }
        public string? PreAnestheticMedicationName { get; private set; }
        public string? PreAnestheticMedicationDose { get; private set; }
        public string? PreAnestheticMedicationRoute { get; private set; }
        public string? PreAnestheticMedicationOtherRoute { get; private set; }
        public TimeOnly? PreAnestheticMedicationTime { get; private set; }
        #endregion

        #region Dor
        public bool? DorUsouENV { get; private set; }
        public int? DorENV { get; private set; }
        public bool? DorUsouPAINAD { get; private set; }
        public int? DorPAINAD { get; private set; }
        public bool? DorUsouBPS { get; private set; }
        public int? DorBPS { get; private set; }
        public string? Conduta { get; private set; }
        #endregion

        #region Antibióticos
        public List<AnesthesiaRecordAntibiotic> Antibiotics { get; private set; } = [];
        public bool? ProphylacticAntibioticUsed { get; private set; }
        #endregion

        #region Sinais Vitais
        public string? BloodPressure { get; private set; }
        public int? RespiratoryRate { get; private set; }
        public decimal? Temperature { get; private set; }
        public int? OxygenSaturation { get; private set; }
        public decimal? WeightKg { get; private set; }
        public AsaClassificationEnum? AsaClassification { get; private set; }
        #endregion

        #region Horários
        public TimeOnly? RoomEntryTime { get; private set; }
        public TimeOnly? AnesthesiaStartTime { get; private set; }
        public TimeOnly? SurgeryEndTime { get; private set; }
        public TimeOnly? AnesthesiaEndTime { get; private set; }
        #endregion

        #region Equipe
        public int? SurgeonId { get; private set; }
        public User? Surgeon { get; private set; }
        public int? AssistantId { get; private set; }
        public User? Assistant { get; private set; }
        public int? FirstAnesthesiologistId { get; private set; }
        public User? FirstAnesthesiologist { get; private set; }
        public int? SecondAnesthesiologistId { get; private set; }
        public User? SecondAnesthesiologist { get; private set; }
        #endregion

        #region Procedimento
        public string? PreOperativeDiagnosis { get; private set; }
        public SurgicalPositionEnum? SurgicalPosition { get; private set; }
        public string? OtherSurgicalPosition { get; private set; }
        public bool? UsesCushions { get; private set; }
        public string? CushionsAccessLocation { get; private set; }
        public VenousAccessTypeEnum? VenousAccessType { get; private set; }
        public string? OtherVenousAccess { get; private set; }
        public string? VenousAccessLocation { get; private set; }
        public bool? DifficultVenousPuncture { get; private set; }

        public bool? GeneralAnesthesia { get; private set; }
        public RespirationModeEnum? RespirationMode { get; private set; }
        public ControlledVentilationModeEnum? ControlledVentilationMode { get; private set; }
        public bool? Co2AbsorberCircuit { get; private set; }
        #endregion

        #region Via Aérea - Dispositivos
        public List<AnesthesiaRecordAirwayDevice> AirwayDevices { get; private set; } = [];
        public string? AirwayDeviceNumbers { get; private set; }
        public bool? Cuff { get; private set; }
        public bool? Iot { get; private set; }
        public bool? OralTube { get; private set; }
        public bool? NasalTube { get; private set; }
        public IntubationDifficultyEnum? IntubationDifficulty { get; private set; }
        #endregion

        #region Via Aérea - Tipo
        public AirwayTypeEnum? AirwayType { get; private set; }
        public string? OtherAirwayTypeDescription { get; private set; }
        #endregion

        #region Via Aérea - Técnicas
        public bool? Laryngoscopy { get; private set; }
        public bool? RetrogradeTechnique { get; private set; }
        public bool? VideoLaryngoscopy { get; private set; }
        public bool? Bronchofibroscopy { get; private set; }
        public bool? Tracheostomy { get; private set; }
        public bool? HasOtherAirwayTechnique { get; private set; }
        public string? OtherAirwayTechnique { get; private set; }
        #endregion

        #region Bloqueios Espinhais
        public bool? SpinalBlockPerformed { get; private set; }
        public List<AnesthesiaRecordPunctureLevel> PunctureLevels { get; private set; } = [];
        public PuncturePositionEnum? PuncturePosition { get; private set; }
        public bool? SpinalCatheter { get; private set; }
        public bool? SpinalOpioid { get; private set; }
        public int? PunctureCount { get; private set; }
        #endregion

        #region Sedação e Oxigênio
        public bool? SedationPerformed { get; private set; }
        public bool? OxygenSupplementation { get; private set; }
        public List<AnesthesiaRecordOxygenSupplementation> OxygenSupplementationTypes { get; private set; } = [];
        public bool? HasOxygenSupplementationOther { get; private set; }
        public string? OxygenSupplementationOther { get; private set; }
        #endregion

        #region Bloqueio Plexo
        public bool? PlexusBlockPerformed { get; private set; }
        public bool? NeurostimulatorUsed { get; private set; }
        public List<AnesthesiaRecordStimulatedNerve> StimulatedNerves { get; private set; } = [];
        #endregion

        public string? SurgeryPerformed { get; private set; }
        public string? PostOperativeDiagnosis { get; private set; }

        #region Recuperação
        public int? ConsciousnessScore { get; private set; }
        public int? ActivityScore { get; private set; }
        public int? CirculationScore { get; private set; }
        public int? RespirationScore { get; private set; }
        public int? OxygenSaturationScore { get; private set; }
        public int? TotalAldreteKroulikScore { get; private set; }
        public TimeOnly? AldreteEvaluationTime { get; private set; }
        public ClinicalDischargeConditionEnum? ClinicalDischargeCondition { get; private set; }
        public string? DischargeConditionOther { get; private set; }
        public PatientDestinationEnum? Destination { get; private set; }
        public bool? HasPain { get; private set; }
        #endregion

        #region Assinatura
        public DateTime? SignatureDate { get; private set; }
        #endregion

        public MonitoringRecord? MonitoringRecord { get; private set; }
        public string PatientId { get; private set; } = string.Empty;
        public DateTime SurgeryDate { get; private set; }
        public SurgeryStatusEnum Status { get; private set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime LastUpdate { get; protected set; }
        public List<AnesthesiaRecordSurgery> Surgeries { get; protected set; } = [];

        public static AnesthesiaRecord Create(AnesthesiaRecordCommand command, DateTime surgeryDate)
        {
            var entity = new AnesthesiaRecord();
            entity.Id = command.SurgeryId;
            entity.SetValues(command);
            entity.CreatedAt = DateTime.UtcNow;
            entity.LastUpdate = DateTime.UtcNow;
            entity.Status = SurgeryStatusEnum.Scheduled;
            entity.SurgeryDate = surgeryDate;

            return entity;
        }

        public void MarkProceduresCustomized()
        {
            ProceduresCustomized = true;
            LastUpdate = DateTime.UtcNow;
        }

        public void SetPrimaryProcedure(int procedureId)
        {
            ClearPrimaryProcedure();

            var procedure = Surgeries
                .FirstOrDefault(x => x.ProcedureId == procedureId);

            if (procedure != null)
                procedure.SetPrimary(true);

            LastUpdate = DateTime.UtcNow;
        }

        private void ClearPrimaryProcedure()
        {
            foreach (var procedure in Surgeries)
                procedure.SetPrimary(false);
        }

        public void AssignFirstAnesthesiologistId(int? id)
        {
            FirstAnesthesiologistId = id > 0 ? id : null;
            LastUpdate = DateTime.UtcNow;
        }

        public void SetStatus(SurgeryStatusEnum status)
        {
            Status = status;
            LastUpdate = DateTime.UtcNow;
        }

        public void Update(AnesthesiaRecordCommand command)
        {
            SetValues(command);
            LastUpdate = DateTime.UtcNow;
        }

        private void SetValues(AnesthesiaRecordCommand command)
        {
            ProceduresCustomized = true;

            #region Segurança
            PatientIdentifiedBeforeInduction = command.PatientIdentifiedBeforeInduction;
            AnestheticConsentSigned = command.AnestheticConsentSigned;
            AnesthesiaEquipmentChecked = command.AnesthesiaEquipmentChecked;
            SafetyObservations = command.SafetyObservations;
            #endregion

            #region Pré-medicação
            PreAnestheticMedication = command.PreAnestheticMedication;
            PreAnestheticMedicationId = command.PreAnestheticMedicationId;
            PreAnestheticMedicationName = command.PreAnestheticMedicationName;
            PreAnestheticMedicationDose = command.PreAnestheticMedicationDose;
            PreAnestheticMedicationRoute = command.PreAnestheticMedicationRoute;
            PreAnestheticMedicationOtherRoute = command.PreAnestheticMedicationOtherRoute;
            PreAnestheticMedicationTime = command.PreAnestheticMedicationTime;
            #endregion

            #region Dor
            DorUsouENV = command.DorUsouENV;
            DorENV = command.DorENV;
            DorUsouPAINAD = command.DorUsouPAINAD;
            DorPAINAD = command.DorPAINAD;
            DorUsouBPS = command.DorUsouBPS;
            DorBPS = command.DorBPS;
            Conduta = command.Conduta;
            #endregion

            #region Antibióticos
            ProphylacticAntibioticUsed = command.ProphylacticAntibioticUsed;
            Antibiotics.Clear();
            foreach (var antibiotic in command.AntibioticsList)
                Antibiotics.Add(AnesthesiaRecordAntibiotic.Create(antibiotic));
            #endregion

            #region Sinais Vitais
            BloodPressure = command.BloodPressure;
            RespiratoryRate = command.RespiratoryRate;
            Temperature = command.Temperature;
            OxygenSaturation = command.OxygenSaturation;
            WeightKg = command.WeightKg;
            AsaClassification = command.AsaClassification;
            #endregion

            #region Horários
            RoomEntryTime = command.RoomEntryTime;
            AnesthesiaStartTime = command.AnesthesiaStartTime;
            SurgeryEndTime = command.SurgeryEndTime;
            AnesthesiaEndTime = command.AnesthesiaEndTime;
            #endregion

            #region Equipe
            SurgeonId = command.SurgeonId;
            AssistantId = command.AssistantId;
            FirstAnesthesiologistId = command.FirstAnesthesiologistId;
            SecondAnesthesiologistId = command.SecondAnesthesiologistId;
            #endregion

            #region Procedimento
            PreOperativeDiagnosis = command.PreOperativeDiagnosis;
            SurgicalPosition = command.SurgicalPosition;
            OtherSurgicalPosition = command.OtherSurgicalPosition;
            UsesCushions = command.UsesCushions;
            CushionsAccessLocation = command.CushionsAccessLocation;
            VenousAccessType = command.VenousAccessType;
            OtherVenousAccess = command.OtherVenousAccess;
            VenousAccessLocation = command.VenousAccessLocation;
            DifficultVenousPuncture = command.DifficultVenousPuncture;

            GeneralAnesthesia = command.GeneralAnesthesia;
            RespirationMode = command.RespirationMode;
            ControlledVentilationMode = command.ControlledVentilationMode;
            Co2AbsorberCircuit = command.Co2AbsorberCircuit;
            #endregion

            #region Via Aérea - Dispositivos
            AirwayDevices.Clear();
            foreach (var deviceType in command.AirwayDeviceType)
                AirwayDevices.Add(AnesthesiaRecordAirwayDevice.Create(deviceType));

            AirwayDeviceNumbers = command.AirwayDeviceNumbers;
            Cuff = command.Cuff;
            Iot = command.Iot;
            OralTube = command.OralTube;
            NasalTube = command.NasalTube;
            IntubationDifficulty = command.IntubationDifficulty;
            #endregion

            #region Via Aérea - Tipo
            AirwayType = command.AirwayType;
            OtherAirwayTypeDescription = command.OtherAirwayTypeDescription;
            #endregion

            #region Via Aérea - Técnicas
            Laryngoscopy = command.Laryngoscopy;
            RetrogradeTechnique = command.RetrogradeTechnique;
            VideoLaryngoscopy = command.VideoLaryngoscopy;
            Bronchofibroscopy = command.Bronchofibroscopy;
            Tracheostomy = command.Tracheostomy;
            HasOtherAirwayTechnique = command.HasOtherAirwayTechnique;
            OtherAirwayTechnique = command.OtherAirwayTechnique;
            #endregion

            #region Bloqueios Espinhais
            SpinalBlockPerformed = command.SpinalBlockPerformed;

            PunctureLevels.Clear();
            foreach (var level in command.PunctureLevels)
                PunctureLevels.Add(AnesthesiaRecordPunctureLevel.Create(level));

            PuncturePosition = command.PuncturePosition;
            SpinalCatheter = command.SpinalCatheter;
            SpinalOpioid = command.SpinalOpioid;
            PunctureCount = command.PunctureCount;
            #endregion

            #region Sedação e Oxigênio
            SedationPerformed = command.SedationPerformed;
            OxygenSupplementation = command.OxygenSupplementation;

            OxygenSupplementationTypes.Clear();

            foreach (var type in command.OxygenSupplementationTypes)
                OxygenSupplementationTypes.Add(AnesthesiaRecordOxygenSupplementation.Create(type));

            HasOxygenSupplementationOther = command.HasOxygenSupplementationOther;
            OxygenSupplementationOther = command.OxygenSupplementationOther;
            #endregion

            #region Bloqueio Plexo
            PlexusBlockPerformed = command.PlexusBlockPerformed;
            NeurostimulatorUsed = command.NeurostimulatorUsed;

            StimulatedNerves.Clear();

            foreach (var nerve in command.StimulatedNerves)
                StimulatedNerves.Add(AnesthesiaRecordStimulatedNerve.Create(nerve));
            #endregion

            SurgeryPerformed = command.SurgeryPerformed;
            PostOperativeDiagnosis = command.PostOperativeDiagnosis;

            #region Recuperação
            ConsciousnessScore = command.ConsciousnessScore;
            ActivityScore = command.ActivityScore;
            CirculationScore = command.CirculationScore;
            RespirationScore = command.RespirationScore;
            OxygenSaturationScore = command.OxygenSaturationScore;
            TotalAldreteKroulikScore = command.TotalAldreteKroulikScore;
            AldreteEvaluationTime = command.AldreteEvaluationTime;
            ClinicalDischargeCondition = command.ClinicalDischargeCondition;
            DischargeConditionOther = command.DischargeConditionOther;
            Destination = command.Destination;
            HasPain = command.HasPain;
            #endregion

            #region Assinatura
            SignatureDate = command.SignatureDate.HasValue
                ? DateTime.SpecifyKind(command.SignatureDate.Value, DateTimeKind.Utc)
                : null;
            #endregion

            PatientId = command.PatientId;
        }

        public void AddProcedures(IEnumerable<SurgeryCommand> surgeries, IEnumerable<Procedure> procedures)
        {
            Surgeries.Clear();

            var proceduresByExternalId = procedures.ToDictionary(x => x.ExternalId);

            foreach (var surgery in surgeries)
            {
                if (string.IsNullOrWhiteSpace(surgery.Id))
                    continue;

                if (!proceduresByExternalId.TryGetValue(surgery.Id, out var procedure))
                    throw new Exception($"Procedimento {surgery.Id} não encontrado.");

                Surgeries.Add(AnesthesiaRecordSurgery.Create(Id, procedure.Id, surgery.IsPrimary, string.IsNullOrWhiteSpace(surgery.Time) ? null : TimeOnly.Parse(surgery.Time)));
            }
        }
    }
}