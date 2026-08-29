using System.ComponentModel;

namespace UFF.FichaAnestesica.Domain.Enums
{
    public enum DrugCategoryEnum
    {
        [Description("Outros")] Outros = 0,
        [Description("Medicamento")] Medicamento = 1,
        [Description("Antibiótico")] Antibiotico = 2,
        [Description("Anestésico")] Anestesico = 3,
        [Description("Analgésico")] Analgesico = 4,
        [Description("Sedativo")] Sedativo = 5,
        [Description("Bloqueador Neuromuscular")] BloqueadorNeuromuscular = 6,
        [Description("Vasopressor")] Vasopressor = 7,
        [Description("Antiemético")] Antiemetico = 8,
        [Description("Diluente")] Diluente = 9,
        [Description("Solução")] Solucao = 10,
        [Description("Material")] Material = 11,
        [Description("Gás Medicinal")] GasMedicinal = 12
    }
}
