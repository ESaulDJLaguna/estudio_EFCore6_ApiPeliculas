//!=>Referencia [1] [Sección 03, 055. Ejecución Diferida]

//!=> [1] REPRESENTA LOS DIFERENTES VALORES POR LOS QUE SE QUIERE FILTRAR
namespace EFCorePeliculas.Entidades.DTOs
{
    public class PeliculasFiltroDTO
    {
        public string Titulo { get; set; }
        public int GeneroId { get; set; }
        public bool EnCartelera { get; set; }
        public bool ProximosEstrenos { get; set; }
    }
}
