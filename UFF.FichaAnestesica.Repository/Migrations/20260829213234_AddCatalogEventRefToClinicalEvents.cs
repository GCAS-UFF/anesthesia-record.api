using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddCatalogEventRefToClinicalEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "catalog_event_id",
                schema: "siga_db",
                table: "clinical_events",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "catalog_event_name",
                schema: "siga_db",
                table: "clinical_events",
                type: "varchar(150)",
                nullable: true);

            // Migra os eventos até então hardcoded no frontend (ClinicalEventTypeEnum) para o
            // catálogo administrável de "Manutenção de Eventos", sem perder nenhum deles.
            // Usa ON CONFLICT (name) DO NOTHING e deixa o id ser gerado pela identity column,
            // pois pode já existir uma linha com o mesmo nome cadastrada manualmente pelo admin.
            migrationBuilder.Sql(@"
                INSERT INTO siga_db.event_types (name, description, active, created_at, last_update) VALUES
                    ('Intubação', 'Intubação orotraqueal realizada.', TRUE, NOW(), NOW()),
                    ('Extubação', 'Extubação realizada.', TRUE, NOW(), NOW()),
                    ('Incisão', 'Incisão cirúrgica realizada.', TRUE, NOW(), NOW()),
                    ('Bloqueio', 'Bloqueio anestésico realizado.', TRUE, NOW(), NOW()),
                    ('Garrote ON', 'Garrote pneumático acionado (ON).', TRUE, NOW(), NOW()),
                    ('Garrote OFF', 'Garrote pneumático liberado (OFF).', TRUE, NOW(), NOW()),
                    ('Posição', 'Alteração de posicionamento do paciente.', TRUE, NOW(), NOW()),
                    ('Complicação', 'Intercorrência registrada durante o procedimento.', TRUE, NOW(), NOW()),
                    ('Outro', 'Evento clínico registrado.', TRUE, NOW(), NOW())
                ON CONFLICT (name) DO NOTHING;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Não remove as linhas de event_types no rollback: como o INSERT acima usa
            // ON CONFLICT DO NOTHING, não há como distinguir com segurança as linhas criadas
            // por esta migration de linhas já cadastradas manualmente pelo admin com o mesmo nome.

            migrationBuilder.DropColumn(
                name: "catalog_event_id",
                schema: "siga_db",
                table: "clinical_events");

            migrationBuilder.DropColumn(
                name: "catalog_event_name",
                schema: "siga_db",
                table: "clinical_events");
        }
    }
}
