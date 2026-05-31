using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Domain.Response
{
    public class MonitoringRecordResponse
    {
        public int Id { get; set; }

        public int AnesthesiaRecordId { get; set; }

        public int SurgeryId { get; set; }

        public int RecordedByProfessionalId { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime? EndedAt { get; set; }

        public List<VitalSignRecordResponse> VitalSigns { get; set; } = new();

        public List<AdministeredAgentResponse> AdministeredAgents { get; set; } = new();

        public List<ClinicalEventResponse> ClinicalEvents { get; set; } = new();

        public List<FluidBalanceResponse> FluidBalances { get; set; } = new();

        public static MonitoringRecordResponse ToResponse(
            MonitoringRecord entity)
        {
            return new MonitoringRecordResponse
            {
                Id = entity.Id,
                AnesthesiaRecordId = entity.AnesthesiaRecordId,                
                RecordedByProfessionalId = entity.RecordedByProfessionalId,
                StartedAt = entity.StartedAt,
                EndedAt = entity.EndedAt,

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
                    .ToList()
            };
        }
    }
}