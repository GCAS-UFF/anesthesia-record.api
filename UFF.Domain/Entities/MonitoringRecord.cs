using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class MonitoringRecord : Base
    {
   
        public int SurgeryId { get; private set; }
        public int RecordedByProfessionalId { get; private set; }
        public DateTime StartedAt { get; private set; }
        public DateTime? EndedAt { get; private set; }
        public List<VitalSignRecord> VitalSigns { get; private set; } = new();
        public List<AdministeredAgent> AdministeredAgents { get; private set; } = new();
        public List<ClinicalEvent> ClinicalEvents { get; private set; } = new();
        public List<FluidBalance> FluidBalances { get; private set; } = new();
        public int AnesthesiaRecordId { get; private set; }
        public AnesthesiaRecord AnesthesiaRecord { get; private set; }

        public static MonitoringRecord Create(MonitoringRecordCommand command)
        {
            var monitoringRecord = new MonitoringRecord
            {
                AnesthesiaRecordId = command.AnesthesiaRecordId,
                SurgeryId = command.SurgeryId,
                RecordedByProfessionalId = command.RecordedByProfessionalId,
                StartedAt = command.StartedAt,
                EndedAt = command.EndedAt,
                CreatedAt = DateTime.UtcNow
            };

            if (command.VitalSigns != null)
            {
                monitoringRecord.VitalSigns = command.VitalSigns
                    .Select(VitalSignRecord.Create)
                    .ToList();
            }

            if (command.AdministeredAgents != null)
            {
                monitoringRecord.AdministeredAgents = command.AdministeredAgents
                    .Select(AdministeredAgent.Create)
                    .ToList();
            }

            if (command.ClinicalEvents != null)
            {
                monitoringRecord.ClinicalEvents = command.ClinicalEvents
                    .Select(ClinicalEvent.Create)
                    .ToList();
            }

            if (command.FluidBalances != null)
            {
                monitoringRecord.FluidBalances = command.FluidBalances
                    .Select(FluidBalance.Create)
                    .ToList();
            }

            return monitoringRecord;
        }

        public void Update(MonitoringRecordCommand command)
        {
            AnesthesiaRecordId = command.AnesthesiaRecordId;
            SurgeryId = command.SurgeryId;
            RecordedByProfessionalId = command.RecordedByProfessionalId;
            StartedAt = command.StartedAt;
            EndedAt = command.EndedAt;

            VitalSigns.Clear();

            foreach (var vitalSign in command.VitalSigns)
            {
                VitalSigns.Add(VitalSignRecord.Create(vitalSign));
            }

            AdministeredAgents.Clear();

            foreach (var administeredAgent in command.AdministeredAgents)
            {
                AdministeredAgents.Add(
                    AdministeredAgent.Create(administeredAgent));
            }

            ClinicalEvents.Clear();

            foreach (var clinicalEvent in command.ClinicalEvents)
            {
                ClinicalEvents.Add(ClinicalEvent.Create(clinicalEvent));
            }

            FluidBalances.Clear();

            foreach (var fluidBalance in command.FluidBalances)
            {
                FluidBalances.Add(FluidBalance.Create(fluidBalance));
            }

            LastUpdate = DateTime.UtcNow;
        }
    }
}