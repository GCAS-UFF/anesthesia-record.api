using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Domain.Response
{
    public class PreAnesthesiaSurgeryResponse
    {
        public int Id { get; set; }        
        public string? ProcedureId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }

        public static PreAnesthesiaSurgeryResponse ToResponse(PreAnesthesiaSurgery entity)
        {
            return new PreAnesthesiaSurgeryResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                IsPrimary = entity.IsPrimary
            };
        }

        public static PreAnesthesiaSurgeryResponse ToResponse(AnesthesiaRecordSurgery relation)
        {
            return new PreAnesthesiaSurgeryResponse
            {
                ProcedureId = relation.Procedure?.ExternalId,
                Name = relation.Procedure?.Description ?? string.Empty,
                IsPrimary = relation.IsPrimary
            };
        }

        /// <summary>
        /// Procedimentos exibidos na pré-anestésica: o procedimento oficial da cirurgia quando o médico
        /// já o definiu no SIGA (na pré-anestésica ou na ficha anestésica); senão, os itens gravados na
        /// própria avaliação (registros anteriores ao vínculo com o catálogo).
        /// </summary>
        public static List<PreAnesthesiaSurgeryResponse> FromRecord(PreAnesthesiaRecord entity)
        {
            var anesthesiaRecord = entity.AnesthesiaRecord;

            if (anesthesiaRecord != null && anesthesiaRecord.HasOfficialProcedures)
            {
                return anesthesiaRecord.Surgeries
                    .OrderByDescending(x => x.IsPrimary)
                    .Select(ToResponse)
                    .ToList();
            }

            return entity.Surgeries.Select(ToResponse).ToList();
        }
    }
}
