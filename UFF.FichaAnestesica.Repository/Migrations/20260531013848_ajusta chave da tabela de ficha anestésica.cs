using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ajustachavedatabeladefichaanestésica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_administered_agents__anesthesia_records_anesthesia_record_id",
                schema: "siga_db",
                table: "administered_agents");

            migrationBuilder.DropForeignKey(
                name: "f_k_monitoring_records_anesthesia_records_anesthesia_record_id",
                schema: "siga_db",
                table: "monitoring_records");

            migrationBuilder.DropPrimaryKey(
                name: "PK_anesthesia_records",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropColumn(
                name: "id",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.AlterColumn<int>(
                name: "surgery_id",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_anesthesia_records",
                schema: "siga_db",
                table: "anesthesia_records",
                column: "surgery_id");

            migrationBuilder.AddForeignKey(
                name: "f_k_administered_agents__anesthesia_records_anesthesia_record_id",
                schema: "siga_db",
                table: "administered_agents",
                column: "anesthesia_record_id",
                principalSchema: "siga_db",
                principalTable: "anesthesia_records",
                principalColumn: "surgery_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_anesthesia_records__monitoring_records_monitoring_record_id",
                schema: "siga_db",
                table: "monitoring_records",
                column: "anesthesia_record_id",
                principalSchema: "siga_db",
                principalTable: "anesthesia_records",
                principalColumn: "surgery_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_administered_agents__anesthesia_records_anesthesia_record_id",
                schema: "siga_db",
                table: "administered_agents");

            migrationBuilder.DropForeignKey(
                name: "f_k_anesthesia_records__monitoring_records_monitoring_record_id",
                schema: "siga_db",
                table: "monitoring_records");

            migrationBuilder.DropPrimaryKey(
                name: "PK_anesthesia_records",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.AlterColumn<int>(
                name: "surgery_id",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "id",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_anesthesia_records",
                schema: "siga_db",
                table: "anesthesia_records",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "f_k_administered_agents__anesthesia_records_anesthesia_record_id",
                schema: "siga_db",
                table: "administered_agents",
                column: "anesthesia_record_id",
                principalSchema: "siga_db",
                principalTable: "anesthesia_records",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_monitoring_records_anesthesia_records_anesthesia_record_id",
                schema: "siga_db",
                table: "monitoring_records",
                column: "anesthesia_record_id",
                principalSchema: "siga_db",
                principalTable: "anesthesia_records",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
