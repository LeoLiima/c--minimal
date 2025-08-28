using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projeto_tarefas.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "monitoria");

            migrationBuilder.CreateTable(
                name: "Moni",
                schema: "monitoria",
                columns: table => new
                {
                    IdMonitor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RA = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apelido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moni", x => x.IdMonitor);
                });

            migrationBuilder.CreateTable(
                name: "Horario",
                schema: "monitoria",
                columns: table => new
                {
                    IdHorario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiaSemana = table.Column<int>(type: "int", nullable: false),
                    horario = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IdMonitor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horario", x => x.IdHorario);
                    table.ForeignKey(
                        name: "FK_Horario_Moni_IdMonitor",
                        column: x => x.IdMonitor,
                        principalSchema: "monitoria",
                        principalTable: "Moni",
                        principalColumn: "IdMonitor",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Horario_IdMonitor",
                schema: "monitoria",
                table: "Horario",
                column: "IdMonitor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Horario",
                schema: "monitoria");

            migrationBuilder.DropTable(
                name: "Moni",
                schema: "monitoria");
        }
    }
}
