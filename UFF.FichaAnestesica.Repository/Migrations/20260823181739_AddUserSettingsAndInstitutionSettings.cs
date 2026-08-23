using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddUserSettingsAndInstitutionSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "institution_settings",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    monitoring_interval_minutes = table.Column<int>(type: "integer", nullable: false, defaultValue: 5),
                    siga_api_url = table.Column<string>(type: "varchar(300)", nullable: true),
                    aghu_api_url = table.Column<string>(type: "varchar(300)", nullable: true),
                    hospital_name = table.Column<string>(type: "varchar(200)", nullable: false, defaultValue: "Hospital Universitário Antônio Pedro"),
                    hospital_sector = table.Column<string>(type: "varchar(200)", nullable: true),
                    hospital_cnpj = table.Column<string>(type: "varchar(20)", nullable: true),
                    hospital_cep = table.Column<string>(type: "varchar(10)", nullable: true),
                    hospital_street = table.Column<string>(type: "varchar(200)", nullable: true),
                    hospital_number = table.Column<string>(type: "varchar(20)", nullable: true),
                    hospital_neighborhood = table.Column<string>(type: "varchar(120)", nullable: true),
                    hospital_city = table.Column<string>(type: "varchar(120)", nullable: false, defaultValue: "Niterói"),
                    hospital_state = table.Column<string>(type: "varchar(2)", nullable: false, defaultValue: "RJ"),
                    updated_by_user_id = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_institution_settings", x => x.id);
                    table.ForeignKey(
                        name: "FK_institution_settings_users_updated_by_user_id",
                        column: x => x.updated_by_user_id,
                        principalSchema: "siga_db",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "user_settings",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    language = table.Column<string>(type: "varchar(10)", nullable: false, defaultValue: "pt-BR"),
                    monitoring_interval_minutes = table.Column<int>(type: "integer", nullable: false, defaultValue: 5),
                    use_institutional_interval = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_settings", x => x.id);
                    table.ForeignKey(
                        name: "f_k_user_settings_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "siga_db",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_institution_settings_updated_by_user_id",
                schema: "siga_db",
                table: "institution_settings",
                column: "updated_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_settings_user_id",
                schema: "siga_db",
                table: "user_settings",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "institution_settings",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "user_settings",
                schema: "siga_db");
        }
    }
}
