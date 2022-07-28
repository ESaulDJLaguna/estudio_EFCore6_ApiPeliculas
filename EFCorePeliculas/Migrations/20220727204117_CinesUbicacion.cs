using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace EFCorePeliculas.Migrations
{
	public partial class CinesUbicacion : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<Point>(
					name: "Ubicacion",
					table: "Cines",
					type: "geography",
					nullable: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
					name: "Ubicacion",
					table: "Cines");
		}
	}
}
