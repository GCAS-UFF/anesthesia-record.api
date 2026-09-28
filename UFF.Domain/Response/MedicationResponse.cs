using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Response
{
    public class MedicationResponse
    {
        public string? Description { get; set; }
        public int Id { get; set; }
        /// <summary>Categoria do cadastro de fármacos (só preenchida na lista de /drugs).</summary>
        public DrugCategoryEnum? Category { get; set; }
    }
}
