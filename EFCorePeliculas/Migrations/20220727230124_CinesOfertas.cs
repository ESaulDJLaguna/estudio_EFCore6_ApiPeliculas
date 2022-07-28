using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCorePeliculas.Migrations
{
	public partial class CinesOfertas : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
					name: "CinesOfertas",
					columns: table => new
					{
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
						FechaFin = table.Column<DateTime>(type: "date", nullable: false),
						PorcentajeDescuento = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
						CineId = table.Column<int>(type: "int", nullable: false)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_CinesOfertas", x => x.Id);
						table.ForeignKey(
											name: "FK_CinesOfertas_Cines_CineId",
											column: x => x.CineId,
											principalTable: "Cines",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateIndex(
					name: "IX_CinesOfertas_CineId",
					table: "CinesOfertas",
					column: "CineId",
					unique: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
					name: "CinesOfertas");
		}
	}
}
