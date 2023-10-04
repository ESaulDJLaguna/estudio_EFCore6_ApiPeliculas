using NetTopologySuite.Geometries;

//!=>Referencia [1] [Sección 02, 022. Almacenando la Ubicación de un Cine - Datos Espaciales]
//!=>Referencia [2] [Sección 02, 024. Relaciones Uno a Uno - Propiedades de Navegación - Ofertas de Cines]
//!=>Referencia [3] [Sección 02, 025. Relaciones Uno a Muchos]
//!=>Referencia [4] [Sección 03, 052. Lazy Loading - Carga Perezosa]

namespace EFCorePeliculas.Entidades
{
	public class Cine
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
        //!=>[1] GUARDA INFORMACIÓN GEOGRÁFICA EN: LATITUD Y LONGITUD
        public Point Ubicacion { get; set; }
        //!=>[2] PROPIEDAD DE NAVEGACIÓN PARA UNA RELACIÓN (1:1). AL OBTENER INFORMACIÓN DE UN Cine, POR MEDIO DE CineOferta RECUPERAMOS SU OFERTA
        //!=>[4] CONFIGURAMOS LA PROPIEDADES DE NAVEGACIÓN COMO virtual PARA USAR LAZY LOADING
        public /*[4] virtual*/ CineOferta CineOferta { get; set; }
        //!=>[3] PROPIEDAD DE NAVEGACIÓN QUE REPRESENTA EL M EN LA RELACIÓN (1:M). RECUPERA UNA COLECCIÓN DE SALAS DE CINES (2D, 3D, etc.) DEL REGISTRO ACTUAL.
        //!=>[3] HashSet<T> ES MÁS RÁPIDO QUE OTRAS COLECCIONES PERO NO PERMITE ORDENAR (DE SER NECESARIO, SE USA ICollection O List)
        //!=>[4] CONFIGURAMOS LA PROPIEDADES DE NAVEGACIÓN COMO virtual PARA USAR LAZY LOADING
        public /*[4] virtual*/ HashSet<SalaDeCine> SalasDeCine { get; set; }
    }
}
