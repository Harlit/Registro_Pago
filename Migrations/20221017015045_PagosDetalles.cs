using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroPago.Migrations
{
    public partial class PagosDetalles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersonaID",
                table: "Pagos",
                newName: "PersonaId");

            migrationBuilder.CreateTable(
                name: "PagosDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PagoId = table.Column<int>(type: "INTEGER", nullable: false),
                    PrestamoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ValorPagado = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagosDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagosDetalles_Pagos_PagoId",
                        column: x => x.PagoId,
                        principalTable: "Pagos",
                        principalColumn: "PagoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PagosDetalles_PagoId",
                table: "PagosDetalles",
                column: "PagoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PagosDetalles");

            migrationBuilder.RenameColumn(
                name: "PersonaId",
                table: "Pagos",
                newName: "PersonaID");
        }
    }
}
