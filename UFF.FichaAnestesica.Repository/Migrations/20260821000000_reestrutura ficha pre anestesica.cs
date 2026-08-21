using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class reestruturafichapreanestesica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // A modelagem anterior de pre_anesthesia_records não correspondia
            // ao que o frontend da ficha pré-anestésica realmente coleta (era
            // um registro de intra/pós-operatório, não uma avaliação
            // pré-anestésica). Confirmado com o usuário que a tabela ainda
            // não está em uso em produção (nenhum Controller/Service a
            // expunha), então a reestruturação abaixo é destrutiva por
            // opção deliberada, não por descuido — ver o resumo desta tarefa.
            migrationBuilder.DropTable(
                name: "pre_anesthesia_records",
                schema: "siga_db");

            migrationBuilder.CreateTable(
                name: "pre_anesthesia_records",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    anesthesia_record_id = table.Column<int>(type: "integer", nullable: false),
                    laterality = table.Column<int>(type: "integer", nullable: true),
                    pre_operative_diagnosis = table.Column<string>(type: "text", nullable: true),
                    consultation_date = table.Column<DateOnly>(type: "date", nullable: true),
                    procedure_observation = table.Column<string>(type: "text", nullable: true),
                    weight_kg = table.Column<decimal>(type: "numeric(6,2)", nullable: true),
                    height_cm = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    bmi = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    heart_rate = table.Column<int>(type: "integer", nullable: true),
                    systolic_blood_pressure = table.Column<int>(type: "integer", nullable: true),
                    diastolic_blood_pressure = table.Column<int>(type: "integer", nullable: true),
                    spo2 = table.Column<int>(type: "integer", nullable: true),
                    temperature = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    fasting_solids_hours = table.Column<decimal>(type: "numeric(4,1)", nullable: true),
                    fasting_liquids_hours = table.Column<decimal>(type: "numeric(4,1)", nullable: true),
                    comorbidities_other_description = table.Column<string>(type: "text", nullable: true),
                    family_history = table.Column<string>(type: "text", nullable: true),
                    illicit_drug_use = table.Column<bool>(type: "boolean", nullable: true),
                    // Corrigido para nullable:false — a entidade sempre inicializa a
                    // lista (List<string> DrugTypes = new()), e o projeto tem
                    // Nullable Reference Types habilitado, então o modelo real do
                    // EF (compilado a partir da entidade) considera esta coluna
                    // NOT NULL. Um "dotnet ef migrations add" local confirmou essa
                    // divergência na minha modelagem original desta migration
                    // (que estava nullable:true) — corrigida aqui.
                    drug_types = table.Column<string[]>(type: "text[]", nullable: false),
                    drugs_other_description = table.Column<string>(type: "text", nullable: true),
                    smoker = table.Column<bool>(type: "boolean", nullable: true),
                    smoking_load = table.Column<string>(type: "text", nullable: true),
                    alcohol_use = table.Column<bool>(type: "boolean", nullable: true),
                    alcohol_grams_per_day = table.Column<string>(type: "text", nullable: true),
                    has_allergy = table.Column<bool>(type: "boolean", nullable: true),
                    allergy_substances = table.Column<string[]>(type: "text[]", nullable: false),
                    allergy_other_description = table.Column<string>(type: "text", nullable: true),
                    allergy_reaction_type = table.Column<string>(type: "text", nullable: true),
                    anesthetic_history = table.Column<string>(type: "text", nullable: true),
                    uses_medication = table.Column<bool>(type: "boolean", nullable: true),
                    airway_mucosa = table.Column<string[]>(type: "text[]", nullable: false),
                    dentition = table.Column<int>(type: "integer", nullable: true),
                    inter_incisor_distance = table.Column<int>(type: "integer", nullable: true),
                    upper_incisor_length = table.Column<int>(type: "integer", nullable: true),
                    mallampati_class = table.Column<int>(type: "integer", nullable: true),
                    incisor_relation = table.Column<int>(type: "integer", nullable: true),
                    palate = table.Column<int>(type: "integer", nullable: true),
                    mandible_protrusion = table.Column<int>(type: "integer", nullable: true),
                    neck_length = table.Column<int>(type: "integer", nullable: true),
                    neck_width = table.Column<int>(type: "integer", nullable: true),
                    sternomental_distance = table.Column<int>(type: "integer", nullable: true),
                    thyromental_distance = table.Column<int>(type: "integer", nullable: true),
                    neck_flexion = table.Column<int>(type: "integer", nullable: true),
                    neck_extension = table.Column<int>(type: "integer", nullable: true),
                    mandibular_space_compliance = table.Column<int>(type: "integer", nullable: true),
                    airway_observations = table.Column<string>(type: "text", nullable: true),
                    thoracic_cage_abnormality = table.Column<bool>(type: "boolean", nullable: true),
                    thoracic_cage_abnormality_description = table.Column<string>(type: "text", nullable: true),
                    difficult_intubation_prediction = table.Column<bool>(type: "boolean", nullable: true),
                    hemoglobin = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    hematocrit = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    leukocytes = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    platelets = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    tap_inr = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    aptt = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    glucose = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    urea = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    creatinine = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    sodium = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    potassium = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    tp = table.Column<string>(type: "text", nullable: true),
                    urinalysis = table.Column<string>(type: "text", nullable: true),
                    liver_function_tests = table.Column<string>(type: "text", nullable: true),
                    pregnancy_test = table.Column<string>(type: "text", nullable: true),
                    ecg = table.Column<string>(type: "text", nullable: true),
                    chest_x_ray = table.Column<string>(type: "text", nullable: true),
                    echocardiogram = table.Column<string>(type: "text", nullable: true),
                    pulmonary_function_test = table.Column<string>(type: "text", nullable: true),
                    other_imaging = table.Column<string>(type: "text", nullable: true),
                    asa_classification = table.Column<int>(type: "integer", nullable: true),
                    is_emergency = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    not_cleared = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    not_cleared_reason = table.Column<string>(type: "text", nullable: true),
                    conduct_actions = table.Column<string[]>(type: "text[]", nullable: false),
                    conduct_notes = table.Column<string>(type: "text", nullable: true),
                    signed_by_professional_id = table.Column<int>(type: "integer", nullable: true),
                    signed_by_name = table.Column<string>(type: "text", nullable: true),
                    signed_at = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    // Corrigido para nullable:false — Base.SaveChanges (SigaDbCtx)
                    // preenche LastUpdate também no INSERT (EntityState.Added), não só
                    // no Update, então a coluna nunca fica nula na prática, e o CLR
                    // type real de Base.LastUpdate é DateTime (não DateTime?), assim
                    // como em todas as outras entidades do projeto (AnesthesiaRecord,
                    // FluidBalance, etc.). Estava nullable:true por engano na primeira
                    // versão desta migration.
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pre_anesthesia_records", x => x.id);
                    table.ForeignKey(
                        // Nome curto e explícito (ver PreAnesthesiaRecordConfig.cs) —
                        // o nome de convenção passava de 63 caracteres (limite do
                        // Postgres) e era truncado/renomeado de forma imprevisível.
                        name: "fk_pre_anesthesia_records_anesthesia_record",
                        column: x => x.anesthesia_record_id,
                        principalSchema: "siga_db",
                        principalTable: "anesthesia_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_pre_anesthesia_records_signed_by_professional",
                        column: x => x.signed_by_professional_id,
                        principalSchema: "siga_db",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pre_anesthesia_surgeries",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    is_primary = table.Column<bool>(type: "boolean", nullable: false),
                    pre_anesthesia_record_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pre_anesthesia_surgeries", x => x.id);
                    table.ForeignKey(
                        name: "fk_pre_anesthesia_surgeries_pre_anesthesia_record",
                        column: x => x.pre_anesthesia_record_id,
                        principalSchema: "siga_db",
                        principalTable: "pre_anesthesia_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pre_anesthesia_comorbidities",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    group_key = table.Column<string>(type: "varchar(50)", nullable: false),
                    findings = table.Column<string[]>(type: "text[]", nullable: false),
                    other_description = table.Column<string>(type: "text", nullable: true),
                    observations = table.Column<string>(type: "text", nullable: true),
                    pre_anesthesia_record_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pre_anesthesia_comorbidities", x => x.id);
                    table.ForeignKey(
                        name: "fk_pre_anesthesia_comorbidities_pre_anesthesia_record",
                        column: x => x.pre_anesthesia_record_id,
                        principalSchema: "siga_db",
                        principalTable: "pre_anesthesia_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pre_anesthesia_physical_exam_areas",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    area_key = table.Column<string>(type: "varchar(50)", nullable: false),
                    findings = table.Column<string[]>(type: "text[]", nullable: false),
                    other_description = table.Column<string>(type: "text", nullable: true),
                    observations = table.Column<string>(type: "text", nullable: true),
                    pre_anesthesia_record_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pre_anesthesia_physical_exam_areas", x => x.id);
                    table.ForeignKey(
                        name: "fk_pre_anesthesia_physical_exam_areas_pre_anesthesia_record",
                        column: x => x.pre_anesthesia_record_id,
                        principalSchema: "siga_db",
                        principalTable: "pre_anesthesia_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pre_anesthesia_medications",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    dose = table.Column<string>(type: "text", nullable: true),
                    route = table.Column<string>(type: "text", nullable: true),
                    frequency = table.Column<string>(type: "text", nullable: true),
                    pre_anesthesia_record_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pre_anesthesia_medications", x => x.id);
                    table.ForeignKey(
                        name: "fk_pre_anesthesia_medications_pre_anesthesia_record",
                        column: x => x.pre_anesthesia_record_id,
                        principalSchema: "siga_db",
                        principalTable: "pre_anesthesia_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pre_anesthesia_reports",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    specialty = table.Column<int>(type: "integer", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    pre_anesthesia_record_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pre_anesthesia_reports", x => x.id);
                    table.ForeignKey(
                        name: "fk_pre_anesthesia_reports_pre_anesthesia_record",
                        column: x => x.pre_anesthesia_record_id,
                        principalSchema: "siga_db",
                        principalTable: "pre_anesthesia_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pre_anesthesia_records_anesthesia_record_id",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                column: "anesthesia_record_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pre_anesthesia_records_signed_by_professional_id",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                column: "signed_by_professional_id");

            migrationBuilder.CreateIndex(
                name: "IX_pre_anesthesia_surgeries_pre_anesthesia_record_id",
                schema: "siga_db",
                table: "pre_anesthesia_surgeries",
                column: "pre_anesthesia_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_pre_anesthesia_comorbidities_pre_anesthesia_record_id",
                schema: "siga_db",
                table: "pre_anesthesia_comorbidities",
                column: "pre_anesthesia_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_pre_anesthesia_physical_exam_areas_pre_anesthesia_record_id",
                schema: "siga_db",
                table: "pre_anesthesia_physical_exam_areas",
                column: "pre_anesthesia_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_pre_anesthesia_medications_pre_anesthesia_record_id",
                schema: "siga_db",
                table: "pre_anesthesia_medications",
                column: "pre_anesthesia_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_pre_anesthesia_reports_pre_anesthesia_record_id",
                schema: "siga_db",
                table: "pre_anesthesia_reports",
                column: "pre_anesthesia_record_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "pre_anesthesia_surgeries", schema: "siga_db");
            migrationBuilder.DropTable(name: "pre_anesthesia_comorbidities", schema: "siga_db");
            migrationBuilder.DropTable(name: "pre_anesthesia_physical_exam_areas", schema: "siga_db");
            migrationBuilder.DropTable(name: "pre_anesthesia_medications", schema: "siga_db");
            migrationBuilder.DropTable(name: "pre_anesthesia_reports", schema: "siga_db");
            migrationBuilder.DropTable(name: "pre_anesthesia_records", schema: "siga_db");

            // Nota: este Down() NÃO recria a estrutura antiga de
            // pre_anesthesia_records (intra/pós-operatório) — ela foi
            // substituída deliberadamente por não corresponder ao contrato
            // real do frontend. Reverter esta migration deixa a tabela
            // ausente; para restaurar a estrutura anterior, reverta até a
            // migration "20260530045009_estrutura inicial" e reaplique as
            // migrations subsequentes sem esta.
        }
    }
}
