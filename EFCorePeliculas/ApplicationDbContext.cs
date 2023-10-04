using EFCorePeliculas.Entidades;
using EFCorePeliculas.Entidades.Configuraciones;
using EFCorePeliculas.Entidades.Seeding;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

//!=>Referencia [1] [Sección 02, 030. Configurando Convenciones]
//!=>Referencia [2] [Sección 02, 031. Organizando el OnModelCreating]
//!=>Referencia [3] [Sección 03, 035. Insertando Datos con Data Seeding]

namespace EFCorePeliculas
{
    public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

        //!=>[1] PARA CONFIGURAR CONVENCIONES SOBREESCRIBIMOS EL MÉTODO ConfigureConventions
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
		{
            //!=>[1] NUEVA CONVENCIÓN: DE AHORA EN ADELANTE LOS TIPOS DateTime SE MAPEARÁN COMO date EN SQL (SE GUARDARÁN SOLO LAS FECHAS Y NO LAS HORAS)
            configurationBuilder.Properties<DateTime>().HaveColumnType("date");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            base.OnModelCreating(modelBuilder);

            //!=>[2] SE CONFIGURABA EL API FLUENTE DE CADA Entidad DE MANERA INDIVIDUAL, SE CAMBIÓ A CONFIGURACIONES INDIVIDUALES
            //modelBuilder.Entity<SalaDeCine>().Property(prop => prop.TipoSalaDeCine).HasDefaultValue(TipoSalaDeCine.DosDimensiones);

            //!=>[2] PRIMERA FORMA DE UTILIZAR LAS CONFIGURACIONES DE LAS Entidades. DEBE CREARSE UN ApplyConfiguration POR CADA CLASE DE CONFIGURACIÓN
            //[2] modelBuilder.ApplyConfiguration(new GeneroConfig());
            //[2] modelBuilder.ApplyConfiguration(new ActorConfig());

            //!=>[2] SEGUNDA FORMA DE UTILIZAR LAS CONFIGURACIONES DE LAS Entidades. "ESCANEA" EL PROYECTO ACTUAL Y TOMA TODAS LAS CLASE QUE HEREDEN DE IEntityTypeConfiguration Y LAS APLICA EN EL API FLUENTE
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			//!=>[3] APLICACIÓN DEL DATA SEEDING. DATOS POR DEFECTO QUE SE INSERTARÁN EN LAS TABLAS AL REALIZAR UNA MIGRACIÓN
			SeedingModuloConsultas.Seed(modelBuilder);
		}


        // UN 'DbContext' REPRESENTA LAS TABLAS EN MI BD: DbSet<T> Nombre {}; 'Nombre' es el que tendrá la tabla en la BD
        public DbSet<Genero> Generos { get; set; }
		public DbSet<Actor> Actores { get; set; }
		public DbSet<Cine> Cines { get; set; }
		public DbSet<Pelicula> Peliculas { get; set; }
		public DbSet<CineOferta> CinesOfertas { get; set; }
		public DbSet<SalaDeCine> SalasDeCine { get; set; }
		public DbSet<PeliculaActor> PeliculasActores { get; set; }
	}
}
