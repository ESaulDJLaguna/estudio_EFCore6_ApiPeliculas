//!=>Referencia [1] [Sección 03, 048.Ordenando y Filtrando la Data Relacionada]

namespace EFCorePeliculas.Entidades.DTOs
{
	public class PeliculaDTO
	{
		public int Id { get; set; }
		public string Titulo { get; set; }
        //!=> [1] GARANTIZA EL ORDENAMIENTO PERO SE SIGUE UTILIZANDO ICollection
        public ICollection<GeneroDTO> Generos { get; set; } = new List<GeneroDTO>();
		public ICollection<CineDTO> Cines { get; set; }
		public ICollection<ActorDTO> Actores{ get; set; }
	}
}
