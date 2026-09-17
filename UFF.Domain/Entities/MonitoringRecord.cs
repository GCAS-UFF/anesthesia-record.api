using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class MonitoringRecord : Base
    {
        public int RecordedByProfessionalId { get; private set; }
        public DateTime? StartedAt { get; private set; }
        public DateTime? EndedAt { get; private set; }
        public List<VitalSignRecord> VitalSigns { get; private set; } = new();
        public List<AdministeredAgent> AdministeredAgents { get; private set; } = new();
        public List<ClinicalEvent> ClinicalEvents { get; private set; } = new();
        public List<FluidBalance> FluidBalances { get; private set; } = new();
        public DateTime? SurgeryStartedAt { get; private set; }
        public DateTime? SurgeryEndedAt { get; private set; }
        public bool IsMonitoringDraft { get; private set; }
        public DateTime? MonitoringUpdatedAt { get; private set; }
        public List<PatientPosition> Positions { get; private set; } = new();
        public List<OxygenFlow> OxygenFlows { get; private set; } = new();
        public List<CompressedAirFlow> CompressedAirFlows { get; private set; } = new();
        public List<InfusionPump> InfusionPumps { get; private set; } = new();
        public int AnesthesiaRecordId { get; private set; }
        public AnesthesiaRecord AnesthesiaRecord { get; private set; }
        public SurgeryStatusEnum Status { get; private set; }

        public static MonitoringRecord Create(MonitoringRecordCommand command)
        {
            var monitoringRecord = new MonitoringRecord
            {
                AnesthesiaRecordId = command.AnesthesiaRecordId,
                RecordedByProfessionalId = command.RecordedByProfessionalId,
                StartedAt = command.StartedAt,
                EndedAt = command.EndedAt,
                SurgeryStartedAt = command.SurgeryStartedAt,
                SurgeryEndedAt = command.SurgeryEndedAt,
                IsMonitoringDraft = command.IsMonitoringDraft,
                MonitoringUpdatedAt = command.MonitoringUpdatedAt,
                Status = command.Status,
                CreatedAt = DateTime.UtcNow
            };


            if (command.VitalSigns != null)
            {
                foreach (var vitalCommand in command.VitalSigns)
                {
                    var vital = VitalSignRecord.Create(vitalCommand);

                    vital.SetMonitoringRecord(monitoringRecord);

                    monitoringRecord.VitalSigns.Add(vital);
                }
            }


            if (command.AdministeredAgents != null)
            {
                foreach (var agentCommand in command.AdministeredAgents)
                {
                    var agent = AdministeredAgent.Create(agentCommand);

                    agent.SetMonitoringRecord(monitoringRecord);

                    monitoringRecord.AdministeredAgents.Add(agent);
                }
            }


            if (command.ClinicalEvents != null)
            {
                foreach (var eventCommand in command.ClinicalEvents)
                {
                    var clinicalEvent = ClinicalEvent.Create(eventCommand);

                    clinicalEvent.SetMonitoringRecord(monitoringRecord);

                    monitoringRecord.ClinicalEvents.Add(clinicalEvent);
                }
            }


            if (command.FluidBalances != null)
            {
                foreach (var fluidCommand in command.FluidBalances)
                {
                    var fluid = FluidBalance.Create(fluidCommand);

                    fluid.SetMonitoringRecord(monitoringRecord);

                    monitoringRecord.FluidBalances.Add(fluid);
                }
            }


            if (command.Positions != null)
            {
                foreach (var positionCommand in command.Positions)
                {
                    var position = PatientPosition.Create(positionCommand);

                    position.SetMonitoringRecord(monitoringRecord);

                    monitoringRecord.Positions.Add(position);
                }
            }


            if (command.OxygenFlows != null)
            {
                foreach (var oxygenCommand in command.OxygenFlows)
                {
                    var oxygenFlow = OxygenFlow.Create(oxygenCommand);

                    oxygenFlow.SetMonitoringRecord(monitoringRecord);

                    monitoringRecord.OxygenFlows.Add(oxygenFlow);
                }
            }


            if (command.CompressedAirFlows != null)
            {
                foreach (var airCommand in command.CompressedAirFlows)
                {
                    var airFlow = CompressedAirFlow.Create(airCommand);

                    airFlow.SetMonitoringRecord(monitoringRecord);

                    monitoringRecord.CompressedAirFlows.Add(airFlow);
                }
            }


            if (command.InfusionPumps != null)
            {
                foreach (var pumpCommand in command.InfusionPumps)
                {
                    var pump = InfusionPump.Create(pumpCommand);

                    pump.SetMonitoringRecord(monitoringRecord);

                    monitoringRecord.InfusionPumps.Add(pump);
                }
            }


            return monitoringRecord;
        }

        public void Update(MonitoringRecordCommand command)
        {
            AnesthesiaRecordId = command.AnesthesiaRecordId;
            RecordedByProfessionalId = command.RecordedByProfessionalId;
            Status = command.Status;
            StartedAt = command.StartedAt;
            EndedAt = command.EndedAt;
            SurgeryStartedAt = command.SurgeryStartedAt;
            SurgeryEndedAt = command.SurgeryEndedAt;
            IsMonitoringDraft = command.IsMonitoringDraft;
            MonitoringUpdatedAt = command.MonitoringUpdatedAt;

            VitalSigns.Clear();

            foreach (var commandVital in command.VitalSigns)
            {
                var vital = VitalSignRecord.Create(commandVital);

                vital.SetMonitoringRecord(this);

                VitalSigns.Add(vital);
            }

            AdministeredAgents.Clear();


            foreach (var commandAgent in command.AdministeredAgents)
            {
                var agent = AdministeredAgent.Create(commandAgent);

                agent.SetMonitoringRecord(this);

                AdministeredAgents.Add(agent);
            }

            ClinicalEvents.Clear();

            foreach (var commandEvent in command.ClinicalEvents)
            {
                var clinicalEvent = ClinicalEvent.Create(commandEvent);

                clinicalEvent.SetMonitoringRecord(this);

                ClinicalEvents.Add(clinicalEvent);
            }

            FluidBalances.Clear();

            foreach (var commandFluid in command.FluidBalances)
            {
                var fluid = FluidBalance.Create(commandFluid);

                fluid.SetMonitoringRecord(this);

                FluidBalances.Add(fluid);
            }

            Positions.Clear();

            foreach (var commandPosition in command.Positions)
            {
                var position = PatientPosition.Create(commandPosition);

                position.SetMonitoringRecord(this);

                Positions.Add(position);
            }

            OxygenFlows.Clear();

            foreach (var commandOxygen in command.OxygenFlows)
            {
                var oxygenFlow = OxygenFlow.Create(commandOxygen);

                oxygenFlow.SetMonitoringRecord(this);

                OxygenFlows.Add(oxygenFlow);
            }

            CompressedAirFlows.Clear();

            foreach (var commandAir in command.CompressedAirFlows)
            {
                var airFlow = CompressedAirFlow.Create(commandAir);

                airFlow.SetMonitoringRecord(this);

                CompressedAirFlows.Add(airFlow);
            }

            InfusionPumps.Clear();

            foreach (var commandPump in command.InfusionPumps)
            {
                var pump = InfusionPump.Create(commandPump);

                pump.SetMonitoringRecord(this);

                InfusionPumps.Add(pump);
            }

            LastUpdate = DateTime.UtcNow;
        }

        public void SetAnesthesiaRecord(AnesthesiaRecord anesthesiaRecord)
            => AnesthesiaRecord = anesthesiaRecord;

        public void SetStatus(SurgeryStatusEnum status)
            => Status = status;
    }
}