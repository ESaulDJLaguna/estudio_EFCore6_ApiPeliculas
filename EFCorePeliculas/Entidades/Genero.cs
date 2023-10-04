using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//!=>Referencia [1] [Sección 02, 016. Llaves Primarias - Configuraciones]
//!=>Referencia [2] [Sección 02, 017. Longitud Máxima de un Campo de Texto]
//!=>Referencia [3] [Sección 02, 018. Campos no Nulos]
//!=>Referencia [4] [Sección 02, 019. Cambiando Nombres y el Esquema]
//!=>Referencia [5] [Sección 02, 028. Relación Muchos a Muchos - Automática]
//!=>Referencia [6] [Sección 02, 028. Relación Muchos a Muchos - Automática]
//!=>Referencia [7] [Sección 03, 052. Lazy Loading - Carga Perezosa]

namespace EFCorePeliculas.Entidades
{
    //!=>[4] NOMBRE DE LA TABLA Y ESQUEMA QUE TDNDRÁ EN LA BASE DE DATOS
    //[4] Table("TablaGeneros", Schema = "Peliculas")]
    public class Genero
    {
        //!=>[1] DIFERENTES FORMAS DE DEFINIR UNA LLAVE PRIMARIA DESDE LOS ATRIBUTOS DE LA PROPIEDAD:
        //[1] public int Id { get; set; } // Por convención
        //[1] public int GeneroId { get; set; } // Usando el mismo nombre de la clase
        //[1] [Key] // De manera explícita se indica qué propiedad será llave primaria
        public int Identificador { get; set; }
        //!=>[2] LIMITA LA LONGITUD DE UN STRING. CON 'String', AMBOS ATRIBUTOS SON VÁLIDOS: StringLength o MaxLength
        //[2] [StringLength(150)]
        //[2] [MaxLength(150)]
        //!=>[3] NO PERMITE GUARDAR NULL EN Nombre
        //[3] [Required]
        //!=>[4] NOMBRE QUE TENDRÁ EL CAMPO EN LA BASE DE DATOS
        //[4] [Column("NombreGenero")]
        public string Nombre { get; set; }
        //!=>[6] PROPIEDAD DE NAVEGACIÓN PARA UNA RELACIÓN (M:M) AUTOMÁTICA (NO EXISTE UNA TABLA INTERMEDIA). RECUPERA TODOS LAS Peliculas DEL Genero ACTUAL
        //!=>[7] CONFIGURAMOS LA PROPIEDADES DE NAVEGACIÓN COMO virtual PARA USAR LAZY LOADING
        public /*[7] virtual*/ HashSet<Pelicula> Peliculas { get; set; }
    }
}
