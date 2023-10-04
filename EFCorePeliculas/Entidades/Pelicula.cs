using Microsoft.EntityFrameworkCore;

//!=>Referencia [1] [Sección 02, 023. Creando la Entidad Películas - Unicode]
//!=>Referencia [2] [Sección 02, 028. Relación Muchos a Muchos - Automática]
//!=>Referencia [3] [Sección 02, 029. Relaciones Muchos a Muchos - Manual]
//!=>Referencia [4] [Sección 03, 048.Ordenando y Filtrando la Data Relacionada]
//!=>Referencia [5] [Sección 03, 052. Lazy Loading - Carga Perezosa]

namespace EFCorePeliculas.Entidades
{
	public class Pelicula
	{
		public int Id { get; set; }
		public string Titulo { get; set; }
		public bool EnCartelera { get; set; }
		public DateTime FechaEstreno { get; set; }
        //!=>[1] NO SE REQUIERE ALMACENAR CARACTERES UNICODE: Ñ, EMOJIS, CARACTERES ÁRABES, ETC.
        //[Unicode(false)]
        public string PosterURL { get; set; }
        //!=>[2] PROPIEDAD DE NAVEGACIÓN PARA UNA RELACIÓN (M:M) AUTOMÁTICA (NO EXISTE UNA TABLA INTERMEDIA). RECUPERA TODOS LOS Generos DEL REGISTRO ACTUAL
        //[2][4]public HashSet<Genero> Generos { get; set; }
        //!=>[4] HashSet<T> NO GARANTIZA QUE SE RESPETE EL ORDENAMIENTO, POR LO QUE SE CAMBIA POR UN List<T>
        //!=>[5] CONFIGURAMOS LA PROPIEDADES DE NAVEGACIÓN COMO virtual PARA USAR LAZY LOADING
        public /*[5] virtual*/ List<Genero> Generos { get; set; }
        //!=>[2] PROPIEDAD DE NAVEGACIÓN PARA UNA RELACIÓN (M:M) AUTOMÁTICA (NO EXISTE UNA TABLA INTERMEDIA). RECUPERA TODOS LAS SalasDeCines DONDE SE PROYECTARÁ
        //!=>[5] CONFIGURAMOS LA PROPIEDADES DE NAVEGACIÓN COMO virtual PARA USAR LAZY LOADING
        public /*[5] virtual*/ HashSet<SalaDeCine> SalasDeCines { get; set; }
        //!=>[3] PROPIEDAD DE NAVEGACIÓN QUE RECUPERA EL CONJUNTO DE INFORMACIÓN RELACIONADA CON LA Película ACTUAL (Actores, Personajes y Orden)
        //!=>[5] CONFIGURAMOS LA PROPIEDADES DE NAVEGACIÓN COMO virtual PARA USAR LAZY LOADING
        public /*[5] virtual*/ HashSet<PeliculaActor> PeliculasActores { get; set; }
	}
}
