namespace UFF.FichaAnestesica.Domain.Helpers
{
    /// <summary>
    /// Conversão defensiva de string para enum, usada para os campos da
    /// avaliação pré-anestésica cujo frontend envia a chave da opção
    /// selecionada como string (ex.: "RIGHT", "NORMAL"), em vez do valor
    /// numérico direto (como já é feito para AsaClassification/Mallampati).
    /// Retorna null em vez de lançar exceção quando o valor é nulo, vazio
    /// ou não corresponde a nenhum membro do enum — evita que a submissão
    /// inteira falhe com 500 por causa de um único campo de seleção não
    /// preenchido pelo usuário.
    /// </summary>
    public static class ParseHelper
    {
        public static TEnum? ParseEnum<TEnum>(string? value) where TEnum : struct, Enum
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            return Enum.TryParse<TEnum>(value, out var parsed) ? parsed : null;
        }
    }
}
