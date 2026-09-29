using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <summary>
    /// Migração só de dados. Até aqui, procedures_customized era marcada em toda criação/atualização
    /// da ficha, mesmo sem nenhum procedimento escolhido. Agora ela significa "o médico definiu o
    /// procedimento no SIGA". Fichas marcadas mas sem nenhum procedimento gravado não têm escolha
    /// médica a preservar: voltam a false para seguir o agendamento do AGHU.
    /// Fichas com procedimento gravado (escolhido pelo médico) não são alteradas.
    /// </summary>
    public partial class NormalizeProceduresCustomizedFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
UPDATE siga_db.anesthesia_records ar
   SET procedures_customized = false
 WHERE ar.procedures_customized = true
   AND NOT EXISTS (
       SELECT 1
         FROM siga_db.anesthesia_record_procedures arp
        WHERE arp.anesthesia_record_id = ar.id
   );");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Sem reversão: o valor anterior da flag nessas fichas não representava escolha médica.
        }
    }
}
