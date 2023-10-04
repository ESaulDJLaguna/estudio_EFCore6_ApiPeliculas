using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

//!=>Referencia [1] [Sección 02, 031. Organizando el OnModelCreating]

namespace EFCorePeliculas.Entidades.Configuraciones
{
    //!=>[1] IEntityTypeConfiguration PERMITE SEPARAR CONFIGURACIONES POR ENTIDAD
    public class CineOfertaConfig : IEntityTypeConfiguration<CineOferta>
	{
		public void Configure(EntityTypeBuilder<CineOferta> builder)
		{
			builder.Property(prop => prop.PorcentajeDescuento)
				// Acepta como máximo 5 dígitos (2 de los cuales son decimales)
				.HasPrecision(precision: 5, scale: 2);
		}
	}
}
