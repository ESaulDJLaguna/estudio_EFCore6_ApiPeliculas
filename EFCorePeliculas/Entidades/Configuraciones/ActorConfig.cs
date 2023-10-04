using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

//!=>Referencia [1] [Sección 02, 020. Creando la Entidad Actor - Mapeando DateTime a Date]
//!=>Referencia [2] [Sección 02, 030. Configurando Convenciones]
//!=>Referencia [3] [Sección 02, 031. Organizando el OnModelCreating]

namespace EFCorePeliculas.Entidades.Configuraciones
{
    //!=>[3] IEntityTypeConfiguration PERMITE SEPARAR CONFIGURACIONES POR ENTIDAD
    public class ActorConfig : IEntityTypeConfiguration<Actor>
	{
		public void Configure(EntityTypeBuilder<Actor> builder)
		{
			builder.Property(prop => prop.Nombre)
				.HasMaxLength(150)
				.IsRequired();
            //!=>[1] DEFINIMOS EL TIPO DE DATO CON EL QUE SE MAPEARÁ EN LA BASE DE DATOS
            //!=>[3] SI SE DEFINIERON CONVENCIONES POR DEFECTO Y UN CAMPO ESPECÍFICO REQUIERE SOBREESCRIBIRLA, SE HACE DE MANERA EXPLÍCITA
            //[1][3] builder.Property(prop => prop.FechaNacimiento).HasColumnType("Date");
        }
    }
}
