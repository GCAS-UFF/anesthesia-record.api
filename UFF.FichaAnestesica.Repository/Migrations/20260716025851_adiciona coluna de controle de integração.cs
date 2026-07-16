using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class adicionacolunadecontroledeintegração : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "procedures_customized",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "procedures_customized",
                schema: "siga_db",
                table: "anesthesia_records");
        }
    }
}
