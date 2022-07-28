using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCorePeliculas.Entidades.Configuraciones
{
	public class GeneroConfig : IEntityTypeConfiguration<Genero>
	{
		public void Configure(EntityTypeBuilder<Genero> builder)
		{
			// Para configurar algún aspecto de la entidad Genero
			builder.HasKey(prop => prop.Identificador);
			builder.Property(prop => prop.Nombre)
				//.HasColumnName("NombreGenero")
				.HasMaxLength(150)
				.IsRequired();
			//builder.ToTable(name: "TablaGeneros", schema: "Peliculas");
		}
	}
}
