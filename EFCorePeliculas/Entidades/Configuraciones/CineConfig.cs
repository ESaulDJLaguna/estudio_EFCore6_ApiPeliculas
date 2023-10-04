using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

//!=>Referencia [1] [Sección 02, 031. Organizando el OnModelCreating]

namespace EFCorePeliculas.Entidades.Configuraciones
{
    //!=>[1] IEntityTypeConfiguration PERMITE SEPARAR CONFIGURACIONES POR ENTIDAD
    public class CineConfig : IEntityTypeConfiguration<Cine>
	{
		public void Configure(EntityTypeBuilder<Cine> builder)
		{
			builder.Property(prop => prop.Nombre)
				.HasMaxLength(150)
				.IsRequired();
		}
	}
}
