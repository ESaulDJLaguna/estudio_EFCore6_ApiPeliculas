//!=>Referencia [1] [Sección 02, 029. Relaciones Muchos a Muchos - Manual]
//!=>Referencia [2] [Sección 03, 052. Lazy Loading - Carga Perezosa]

namespace EFCorePeliculas.Entidades
{
    //!=>[1] TABLA INTERMEDIA QUE REPRESENTA LA RELACIÓ (M:M) ENTRE: Peliculas <=> Actores. SE REQUIERE ESTA ENTIDAD PORQUE SE AGREGARÁ INFORMACIÓN EXTRA
    public class PeliculaActor
	{
		public int PeliculaId { get; set; }
		public int ActorId { get; set; }
		public string Personaje { get; set; }
		public int Orden { get; set; }
        //!=>[1] PROPIEDAD DE NAVEGACIÓN PARA RECUPERAR LA INFORMACIÓN DE LA PELÍCULA PERTENECIENTE A PeliculaId
        //!=>[2] CONFIGURAMOS LA PROPIEDADES DE NAVEGACIÓN COMO virtual PARA USAR LAZY LOADING
        public /*[2] virtual*/ Pelicula Pelicula { get; set; }
        //!=>[1] PROPIEDAD DE NAVEGACIÓN PARA RECUPERAR LA INFORMACIÓN DEL ACTOR PERTENECIENTE A ActorId
        //!=>[2] CONFIGURAMOS LA PROPIEDADES DE NAVEGACIÓN COMO virtual PARA USAR LAZY LOADING
        public /*[2] virtual*/ Actor Actor { get; set; }
	}
}
