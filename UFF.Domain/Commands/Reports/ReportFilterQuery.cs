using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Commands.Reports
{
    public class ReportFilterQuery
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? AnesthesiologistId { get; set; }
        public SurgeryStatusEnum? Status { get; set; }

        public string? Validate()
        {
            if (EndDate.Date < StartDate.Date)
                return "A data final não pode ser anterior à data inicial.";

            if ((EndDate.Date - StartDate.Date).TotalDays > 366)
                return "O período selecionado não pode ser maior que 366 dias.";

            return null;
        }
    }
}
