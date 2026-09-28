using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Extensions;

namespace UFF.FichaAnestesica.Domain.Response
{
    /// <summary>
    /// Fontes de consumo de uma cirurgia (resumo de insumos), reunidas numa única leitura:
    /// monitorização (fármacos, bombas, O2/Ar, balanço hídrico, eventos e horários) e as
    /// medicações registradas na ficha anestésica (pré-anestésica e antibióticos com repiques).
    /// Somente leitura — os totais são consolidados no app, que também consolida o rascunho
    /// local ainda não sincronizado com a mesma regra (uma única implementação do cálculo).
    /// </summary>
    public class SurgeryConsumptionResponse
    {
        public int SurgeryId { get; set; }

        /// <summary>Status da ficha anestésica (Completed = ficha finalizada/assinada).</summary>
        public SurgeryStatusEnum AnesthesiaRecordStatus { get; set; }

        /// <summary>Status da monitorização (Completed = anestesia encerrada).</summary>
        public SurgeryStatusEnum MonitoringStatus { get; set; }

        public int? FirstAnesthesiologistId { get; set; }

        public decimal? WeightKg { get; set; }

        public MonitoringRecordResponse Monitoring { get; set; } = new();

        /// <summary>Categoria (cadastro de fármacos) de cada fármaco usado na monitorização.</summary>
        public Dictionary<int, DrugCategoryEnum> DrugCategories { get; set; } = new();

        public ConsumptionPreAnestheticMedicationResponse? PreAnestheticMedication { get; set; }

        public List<AntibioticResponse> Antibiotics { get; set; } = new();

        public bool? OxygenSupplementation { get; set; }

        public List<string> OxygenSupplementationTypes { get; set; } = new();

        public string? OxygenSupplementationOther { get; set; }

        public static SurgeryConsumptionResponse ToResponse(MonitoringRecord monitoring)
        {
            var record = monitoring.AnesthesiaRecord;

            var drugCategories = new Dictionary<int, DrugCategoryEnum>();
            foreach (var drug in monitoring.AdministeredAgents.Select(a => a.Drug)
                         .Concat(monitoring.InfusionPumps.Select(p => p.Drug)))
            {
                if (drug != null)
                    drugCategories[drug.Id] = drug.Category;
            }

            return new SurgeryConsumptionResponse
            {
                SurgeryId = monitoring.AnesthesiaRecordId,
                AnesthesiaRecordStatus = record?.Status ?? SurgeryStatusEnum.Scheduled,
                MonitoringStatus = monitoring.Status,
                FirstAnesthesiologistId = record?.FirstAnesthesiologistId,
                WeightKg = record?.WeightKg,
                Monitoring = MonitoringRecordResponse.ToResponse(monitoring),
                DrugCategories = drugCategories,
                PreAnestheticMedication = record?.PreAnestheticMedication == true
                    ? new ConsumptionPreAnestheticMedicationResponse
                    {
                        MedicationId = record.PreAnestheticMedicationId,
                        Name = record.PreAnestheticMedicationName,
                        Dose = record.PreAnestheticMedicationDose,
                        Route = string.IsNullOrWhiteSpace(record.PreAnestheticMedicationOtherRoute)
                            ? record.PreAnestheticMedicationRoute
                            : record.PreAnestheticMedicationOtherRoute,
                        Time = record.PreAnestheticMedicationTime
                    }
                    : null,
                Antibiotics = record?.ProphylacticAntibioticUsed == false
                    ? new List<AntibioticResponse>()
                    : (record?.Antibiotics ?? new List<AnesthesiaRecordAntibiotic>())
                        .Select(MapAntibiotic)
                        .ToList(),
                OxygenSupplementation = record?.OxygenSupplementation,
                OxygenSupplementationTypes = record?.OxygenSupplementation == true
                    ? record.OxygenSupplementationTypes.Select(t => t.Type.GetDescription()).ToList()
                    : new List<string>(),
                OxygenSupplementationOther = record?.OxygenSupplementation == true && record.HasOxygenSupplementationOther == true
                    ? record.OxygenSupplementationOther
                    : null
            };
        }

        private static AntibioticResponse MapAntibiotic(AnesthesiaRecordAntibiotic antibiotic) => new()
        {
            MedicationId = antibiotic.MedicationId,
            MedicationName = antibiotic.MedicationName,
            Name = antibiotic.Name,
            Dose = antibiotic.Dose,
            Route = antibiotic.Route,
            Time = antibiotic.Time,
            HasBooster = antibiotic.HasBooster,
            Boosters = antibiotic.Boosters.Select(b => new BoosterResponse
            {
                MedicationId = b.MedicationId,
                MedicationName = b.MedicationName,
                Name = b.Name,
                Dose = b.Dose,
                Route = b.Route,
                Time = b.Time
            }).ToList()
        };
    }

    public class ConsumptionPreAnestheticMedicationResponse
    {
        public int? MedicationId { get; set; }
        public string? Name { get; set; }
        public string? Dose { get; set; }
        public string? Route { get; set; }
        public TimeOnly? Time { get; set; }
    }
}
