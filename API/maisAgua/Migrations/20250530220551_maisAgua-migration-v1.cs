using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maisAgua.Migrations
{
    /// <inheritdoc />
    public partial class maisAguamigrationv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_sensores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nome_dispositivo = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    data_hora = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_sensores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_leituras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nivel_pct = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    turbidez_ntu = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    ph_int = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    data_hora_leitura = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    id_sensor = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_leituras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_leituras_tbl_sensores_id_sensor",
                        column: x => x.id_sensor,
                        principalTable: "tbl_sensores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_leituras_id_sensor",
                table: "tbl_leituras",
                column: "id_sensor");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_sensores_nome_dispositivo",
                table: "tbl_sensores",
                column: "nome_dispositivo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_leituras");

            migrationBuilder.DropTable(
                name: "tbl_sensores");
        }
    }
}
