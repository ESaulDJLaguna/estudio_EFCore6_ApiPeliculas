using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCorePeliculas.Migrations
{
	public partial class PeliculaSalaDeCines : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
					name: "PeliculaSalaDeCine",
					columns: table => new
					{
						PeliculasId = table.Column<int>(type: "int", nullable: false),
						SalasDeCinesId = table.Column<int>(type: "int", nullable: false)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_PeliculaSalaDeCine", x => new { x.PeliculasId, x.SalasDeCinesId });
						table.ForeignKey(
											name: "FK_PeliculaSalaDeCine_Peliculas_PeliculasId",
											column: x => x.PeliculasId,
											principalTable: "Peliculas",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_PeliculaSalaDeCine_SalasDeCine_SalasDeCinesId",
											column: x => x.SalasDeCinesId,
											principalTable: "SalasDeCine",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateIndex(
					name: "IX_PeliculaSalaDeCine_SalasDeCinesId",
					table: "PeliculaSalaDeCine",
					column: "SalasDeCinesId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
					name: "PeliculaSalaDeCine");
		}
	}
}
