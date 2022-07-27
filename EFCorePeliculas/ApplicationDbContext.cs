using EFCorePeliculas.Entidades;
using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculas
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Para configurar algún aspecto de la entidad Genero
			modelBuilder.Entity<Genero>().HasKey(prop => prop.Identificador);
			modelBuilder.Entity<Genero>().Property(prop => prop.Nombre)
				//.HasColumnName("NombreGenero")
				.HasMaxLength(150)
				.IsRequired();
			//modelBuilder.Entity<Genero>().ToTable(name: "TablaGeneros", schema: "Peliculas");

			modelBuilder.Entity<Actor>().Property(prop => prop.Nombre).HasMaxLength(150).IsRequired();
			modelBuilder.Entity<Actor>().Property(prop => prop.FechaNacimiento).HasColumnType("date");

			modelBuilder.Entity<Cine>().Property(prop => prop.Nombre).HasMaxLength(150).IsRequired();
			modelBuilder.Entity<Cine>().Property(prop => prop.Precio).HasPrecision(precision: 9, scale: 2);
		}

		public DbSet<Genero> Generos { get; set; }
		public DbSet<Actor> Actores { get; set; }
		public DbSet<Cine> Cines { get; set; }
	}
}
