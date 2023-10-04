using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

//!=>Referencia [1] [Sección 02, 016. Llaves Primarias - Configuraciones]
//!=>Referencia [2] [Sección 02, 017. Longitud Máxima de un Campo de Texto]
//!=>Referencia [3] [Sección 02, 018. Campos no Nulos]
//!=>Referencia [4] [Sección 02, 019. Cambiando Nombres y el Esquema]
//!=>Referencia [5] [Sección 02, 031. Organizando el OnModelCreating]

namespace EFCorePeliculas.Entidades.Configuraciones
{
    //!=>[5] IEntityTypeConfiguration PERMITE SEPARAR CONFIGURACIONES POR ENTIDAD
    public class GeneroConfig : IEntityTypeConfiguration<Genero>
    {
        // Para configurar algún aspecto de la entidad Genero
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            //!=>[1] SE CONFIGURA Identificador COMO LLAVE PRIMARIA
            builder.HasKey(prop => prop.Identificador);
            //![2] CON Property INDICAMOS LA PROPIEDAD A CONFIGURAR
            builder.Property(prop => prop.Nombre)
                //!=>[4] NOMBRE QUE TENDRÁ EL CAMPO EN LA BASE DE DATOS
                //[4] .HasColumnName("NombreGenero")
                //!=>[2] LONGITUD MÁXIMA DEL CAMPO Nombre
                .HasMaxLength(150)
                //!=>[3] NO PERMITE GUARDAR NULL EN Nombre
                .IsRequired();
            //!=>[4] NOMBRE DE LA TABLA Y ESQUEMA QUE TDNDRÁ EN LA BASE DE DATOS
            //[4] builder.ToTable(name: "TablaGeneros", schema: "Peliculas");
        }
    }
}
