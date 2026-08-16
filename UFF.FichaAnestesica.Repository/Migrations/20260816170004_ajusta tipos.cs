using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ajustatipos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Conversão de unit de text para integer
            migrationBuilder.Sql("""
                ALTER TABLE siga_db.administered_agents
                ALTER COLUMN "unit" TYPE integer
                USING "unit"::integer;
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Conversão de unit de integer para text
            migrationBuilder.Sql("""
                ALTER TABLE siga_db.administered_agents
                ALTER COLUMN "unit" TYPE text
                USING "unit"::text;
            """);
        }
    }
}