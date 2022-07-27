using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCorePeliculas.Entidades
{
	//[Table("TablaGeneros", Schema = "Peliculas")]
	public class Genero
	{
		//public int Id { get; set; } // Por convención, se define como primary key
		//public int GeneroId { get; set; } // Otra forma de definir la primary key (debe tener el mismo nombre que la clase)
		//[Key] // Se requiere sobre la propiedad que será la primary key
		public int Identificador { get; set; }
		// Limita la longitud de un string, cualquiera de los dos atributos es válido
		//[StringLength(150)] // En el caso de un string, hace lo mismo que MaxLength
		//[MaxLength(150)] // En el caso de un string, hace lo mismo que StringLength
		//[Required]
		//[Column("NombreGenero")]
		public string Nombre { get; set; }
	}
}
