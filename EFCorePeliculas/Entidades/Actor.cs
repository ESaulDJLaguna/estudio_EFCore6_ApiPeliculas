using System.ComponentModel.DataAnnotations.Schema;

//!=>Referencia [1] [Sección 02, 020. Creando la Entidad Actor - Mapeando DateTime a Date]
//!=>Referencia [2] [Sección 02, 029. Relaciones Muchos a Muchos - Manual]
//!=>Referencia [3] [Sección 03, 052. Lazy Loading - Carga Perezosa]

namespace EFCorePeliculas.Entidades
{
    public class Actor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Biografia { get; set; }
        //!=>[1] DEFINIMOS EL TIPO DE DATO CON EL QUE SE MAPEARÁ EN LA BASE DE DATOS
        //[1] [Column(TypeName = "Date")]
        public DateTime? FechaNacimiento { get; set; } //!=>[1] PERMITIMOS ALMACENAR NULOS
        //!=>[2] PROPIEDAD DE NAVEGACIÓN QUE RECUPERA EL CONJUNTO DE INFORMACIÓN RELACIONADA CON EL Actor ACTUAL (Películas, Personajes y Orden)
        //!=>[3] CONFIGURAMOS LA PROPIEDADES DE NAVEGACIÓN COMO virtual PARA USAR LAZY LOADING
        public /*[3] virtual*/ HashSet<PeliculaActor> PeliculasActores { get; set; }
    }
}
