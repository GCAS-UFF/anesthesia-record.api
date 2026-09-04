using System.ComponentModel;

namespace UFF.FichaAnestesica.Domain.Enums
{
    public enum OxygenSupplementationTypeEnum
    {
        [Description("Catéter Nasal")] CateterNasal = 1,
        [Description("Máscara Simples")] MascaraSimples = 2,
        [Description("Máscara com Reservatório")] MascaraReservatorio = 3,
        [Description("Venturi")] Venturi = 4,
        [Description("Outros")] Outros = 5
    }
}
