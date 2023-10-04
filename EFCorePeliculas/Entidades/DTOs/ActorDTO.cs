//!=>Referencia [1] [Sección 03, 042. Seleccionando Columnas con Select]

namespace EFCorePeliculas.Entidades.DTOs
{
    //!=>[1] UN 'DTO' SON CLASES QUE SIRVEN PARA ALMACENAR DATOS QUE SE VAN A LLEVER DE UN LUGAR A OTRO
    public class ActorDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public DateTime FechaNacimiento { get; set; }
	}
}
