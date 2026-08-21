using UFF.FichaAnestesica.Domain.Commands.PreAnesthesiaRecord;

namespace UFF.FichaAnestesica.Domain.Entities
{
    /// <summary>
    /// Um dos procedimentos cirúrgicos listados na avaliação pré-anestésica
    /// (campo procedure.surgeries do frontend). Guardado como texto livre,
    /// não como FK para Procedure: o frontend não coleta um ProcedureId real
    /// aqui (usa uma lista de sugestões de autocomplete só para digitação),
    /// então não há id confiável para vincular ao catálogo de Procedure.
    /// </summary>
    public class PreAnesthesiaSurgery : Base
    {
        private PreAnesthesiaSurgery() { }

        public string Name { get; private set; } = default!;
        public bool IsPrimary { get; private set; }

        public int PreAnesthesiaRecordId { get; private set; }
        public PreAnesthesiaRecord PreAnesthesiaRecord { get; private set; } = default!;

        public static PreAnesthesiaSurgery Create(PreAnesthesiaSurgeryCommand command)
        {
            return new PreAnesthesiaSurgery
            {
                Name = command.Name,
                IsPrimary = command.IsPrimary,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void SetPreAnesthesiaRecord(PreAnesthesiaRecord preAnesthesiaRecord)
        {
            PreAnesthesiaRecord = preAnesthesiaRecord;
        }
    }
}
