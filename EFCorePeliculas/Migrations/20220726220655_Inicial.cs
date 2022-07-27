using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCorePeliculas.Migrations
{
	public partial class Inicial : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
					name: "Actores",
					columns: table => new
					{
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
						Biografia = table.Column<string>(type: "nvarchar(max)", nullable: true),
						FechaNacimiento = table.Column<DateTime>(type: "date", nullable: true)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_Actores", x => x.Id);
					});

			migrationBuilder.CreateTable(
					name: "Generos",
					columns: table => new
					{
						Identificador = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_Generos", x => x.Identificador);
					});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
					name: "Actores");

			migrationBuilder.DropTable(
					name: "Generos");
		}
	}
}
