using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace maisAgua.Migrations
{
    /// <inheritdoc />
    public partial class maisAguasupav1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_sensores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome_dispositivo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    data_hora = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_sensores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_leituras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nivel_pct = table.Column<int>(type: "integer", nullable: false),
                    turbidez_ntu = table.Column<float>(type: "real", nullable: false),
                    ph_int = table.Column<float>(type: "real", nullable: false),
                    data_hora_leitura = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    id_sensor = table.Column<int>(type: "integer", nullable: false)
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
