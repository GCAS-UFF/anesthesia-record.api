using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ajustaobrigatoriedades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "role",
                schema: "siga_db",
                table: "users");

            migrationBuilder.AlterColumn<string>(
                name: "sector",
                schema: "siga_db",
                table: "users",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "sector",
                schema: "siga_db",
                table: "users",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "role",
                schema: "siga_db",
                table: "users",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");
        }
    }
}
