using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

//!=>Referencia [1] [Sección 02, 021. Creando la Entidad Cine - Precisión de Decimales]
//!=>Referencia [2] [Sección 02, 027. Valor por Defecto]
//!=>Referencia [3] [Sección 02, 031. Organizando el OnModelCreating]

namespace EFCorePeliculas.Entidades.Configuraciones
{
    //!=>[3] IEntityTypeConfiguration PERMITE SEPARAR CONFIGURACIONES POR ENTIDAD
    public class SalaDeCineConfig : IEntityTypeConfiguration<SalaDeCine>
    {
        public void Configure(EntityTypeBuilder<SalaDeCine> builder)
        {
            builder.Property(prop => prop.Precio)
                //!=>[1] PUEDE GUARDARSE UN NÚMERO DE HASTA 9 DÍGITOS (DE LOS CUALES 2 SON DECIMALES)
                .HasPrecision(precision: 9, scale: 2);
            builder.Property(prop => prop.TipoSalaDeCine)
                //!=>[2] ESTABLECE EL VALOR POR DEFECTO DEL CAMPO, UTILIZANDO UNA EXPRESIÓN SQL
                //.HasDefaultValueSql("GETDATE()")
                //!=>[2] ESTABLECE EL VALOR POR DEFECTO QUE TENDRÁ EL CAMPO TipoSalaDeCine
                .HasDefaultValue(TipoSalaDeCine.DosDimensiones);
        }
    }
}
