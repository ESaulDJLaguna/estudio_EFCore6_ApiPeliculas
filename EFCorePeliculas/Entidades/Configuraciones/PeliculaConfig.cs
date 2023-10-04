using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

//!=>Referencia [1] [Sección 02, 023. Creando la Entidad Películas - Unicode]
//!=>Referencia [2] [Sección 02, 031. Organizando el OnModelCreating]

namespace EFCorePeliculas.Entidades.Configuraciones
{
    //!=>[2] IEntityTypeConfiguration PERMITE SEPARAR CONFIGURACIONES POR ENTIDAD
    public class PeliculaConfig : IEntityTypeConfiguration<Pelicula>
	{
		public void Configure(EntityTypeBuilder<Pelicula> builder)
		{
			builder.Property(prop => prop.Titulo)
				.HasMaxLength(250)
				.IsRequired();
			builder.Property(prop => prop.PosterURL)
				.HasMaxLength(500)
                //!=>[1] NO SE REQUIERE ALMACENAR CARACTERES UNICODE: Ñ, EMOJIS, CARACTERES ÁRABES, ETC.
                .IsUnicode(false);
		}
	}
}
