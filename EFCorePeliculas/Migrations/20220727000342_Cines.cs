using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCorePeliculas.Migrations
{
	public partial class Cines : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
					name: "Cines",
					columns: table => new
					{
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
						Precio = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_Cines", x => x.Id);
					});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
					name: "Cines");
		}
	}
}
