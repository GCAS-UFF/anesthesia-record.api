using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class tornaanestesistasnullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "first_anesthesiologist_id",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "first_anesthesiologist_id",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
