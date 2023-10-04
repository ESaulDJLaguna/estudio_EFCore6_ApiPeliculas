using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

//!=>Referencia [1] [Sección 02, 029. Relaciones Muchos a Muchos - Manual]
//!=>Referencia [2] [Sección 02, 031. Organizando el OnModelCreating]

namespace EFCorePeliculas.Entidades.Configuraciones
{
    //!=>[3] IEntityTypeConfiguration PERMITE SEPARAR CONFIGURACIONES POR ENTIDAD
    public class PeliculaActorConfig : IEntityTypeConfiguration<PeliculaActor>
	{
		public void Configure(EntityTypeBuilder<PeliculaActor> builder)
		{
            //!=>[1] TIENE UNA LLAVE COMPUESTA POR PeliculaId Y ActorId
            builder.HasKey(prop => new { prop.PeliculaId, prop.ActorId });
			builder.Property(prop => prop.Personaje).HasMaxLength(150);
		}
	}
}
