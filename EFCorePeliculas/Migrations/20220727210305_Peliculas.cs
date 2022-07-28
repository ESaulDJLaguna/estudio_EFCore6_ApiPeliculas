using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCorePeliculas.Migrations
{
	public partial class Peliculas : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
					name: "Peliculas",
					columns: table => new
					{
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						Titulo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
						EnCartelera = table.Column<bool>(type: "bit", nullable: false),
						FechaEstreno = table.Column<DateTime>(type: "date", nullable: false),
						PosterURL = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_Peliculas", x => x.Id);
					});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
					name: "Peliculas");
		}
	}
}
