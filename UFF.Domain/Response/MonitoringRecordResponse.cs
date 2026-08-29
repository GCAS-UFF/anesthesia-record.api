using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Response
{
    public class MonitoringRecordResponse
    {
        public int Id { get; set; }

        public int AnesthesiaRecordId { get; set; }

        public int SurgeryId { get; set; }

        public int? FirstAnesthesiologistId { get; set; }

        public int RecordedByProfessionalId { get; set; }

        public SurgeryStatusEnum Status { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime? EndedAt { get; set; }


        public DateTime SurgeryStartedAt { get; set; }

        public DateTime? SurgeryEndedAt { get; set; }

        public List<VitalSignRecordResponse> VitalSigns { get; set; } = new();

        public List<AdministeredAgentResponse> AdministeredAgents { get; set; } = new();

        public List<ClinicalEventResponse> ClinicalEvents { get; set; } = new();

        public List<FluidBalanceResponse> FluidBalances { get; set; } = new();

        public List<PatientPositionResponse> Positions { get; set; } = new();

        public static MonitoringRecordResponse ToResponse(
            MonitoringRecord entity)
        {
            return new MonitoringRecordResponse
            {
                Id = entity.Id,
                AnesthesiaRecordId = entity.AnesthesiaRecordId,
                SurgeryId = entity.AnesthesiaRecordId,
                FirstAnesthesiologistId = entity.AnesthesiaRecord?.FirstAnesthesiologistId,
                Status = entity.Status,
                RecordedByProfessionalId = entity.RecordedByProfessionalId,
                StartedAt = entity.StartedAt,
                EndedAt = entity.EndedAt,
                SurgeryStartedAt = entity.SurgeryStartedAt ?? default,
                SurgeryEndedAt = entity.SurgeryEndedAt ?? default,
                VitalSigns = entity.VitalSigns
                    .Select(VitalSignRecordResponse.ToResponse)
                    .ToList(),
                AdministeredAgents = entity.AdministeredAgents
                    .Select(AdministeredAgentResponse.ToResponse)
                    .ToList(),
                ClinicalEvents = entity.ClinicalEvents
                    .Select(ClinicalEventResponse.ToResponse)
                    .ToList(),

                FluidBalances = entity.FluidBalances
                    .Select(FluidBalanceResponse.ToResponse)
                    .ToList(),

                Positions = entity.Positions
                    .Select(PatientPositionResponse.ToResponse)
                    .ToList()
            };
        }
    }
}