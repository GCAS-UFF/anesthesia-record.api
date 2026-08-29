using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Commands.Drugs
{
    public class UpdateDrugCategoryCommand
    {
        public DrugCategoryEnum Category { get; set; }
    }
}
