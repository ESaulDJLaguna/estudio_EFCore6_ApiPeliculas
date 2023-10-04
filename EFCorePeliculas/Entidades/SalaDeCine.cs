using Microsoft.EntityFrameworkCore;

//!=>Referencia [1] [Sección 02, 021. Creando la Entidad Cine - Precisión de Decimales]
//!=>Referencia [2] [Sección 02, 025. Relaciones Uno a Muchos]
//!=>Referencia [3] [Sección 02, 026. Utilizando Enums]
//!=>Referencia [4] [Sección 02, 028. Relación Muchos a Muchos - Automática]
//!=>Referencia [5] [Sección 03, 052. Lazy Loading - Carga Perezosa]

namespace EFCorePeliculas.Entidades
{
    //!=>[2] RELACIÓN (1:M). 1 CINE TENDRÁ VARIOS TIPOS DE SALAS DE CINE (2D, 3D, etc.). INCLUYE EL PRECIO DEL BOLETO
    public class SalaDeCine
	{
		public int Id { get; set; }
        //!=>[3] UNA Enum SE GUARDA COMO UN int EN BASE DE DATOS
        public TipoSalaDeCine TipoSalaDeCine { get; set; }
        //!=>[1] PUEDE GUARDARSE UN NÚMERO DE HASTA 9 DÍGITOS (DE LOS CUALES 2 SON DECIMALES)
        //[1] [Precision(precision: 9, scale: 2)]
        public decimal Precio { get; set; }
        //!=>[2] LLAVE FORÁNEA A LA TABLA Cine, QUE INDICA QUÉ CINE TENDRÁ LA INFORMACIÓN EN EL REGISTRO
        public int CineId { get; set; }
        //!=>[2] PROPIEDAD DE NAVEGACIÓN QUE REPRESENTA EL 1 EN LA RELACIÓN (1:M). RECUPERA LA INFORMACIÓN DEL Cine DEL REGISTRO ACTUAL
        //!=>[5] CONFIGURAMOS LA PROPIEDADES DE NAVEGACIÓN COMO virtual PARA USAR LAZY LOADING
        public /*[5] virtual*/ Cine Cine { get; set; }
        //!=>[4] PROPIEDAD DE NAVEGACIÓN PARA UNA RELACIÓN (M:M) AUTOMÁTICA (NO EXISTE UNA TABLA INTERMEDIA). RECUPERA Peliculas A PROYECTARSE EN LA SALA ACTUAL
        //!=>[5] CONFIGURAMOS LA PROPIEDADES DE NAVEGACIÓN COMO virtual PARA USAR LAZY LOADING
        public /*[5] virtual*/ HashSet<Pelicula> Peliculas { get; set; }
	}
}
