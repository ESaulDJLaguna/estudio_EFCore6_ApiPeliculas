//!=>Referencia [1] [Sección 03, 044. Consultando Datos Espaciales]

namespace EFCorePeliculas.Entidades.DTOs
{
    //!=>[1] DTO PARA CINES QUE "DESGLOSA" EL TIPO Point EN LATITUD Y LONGITUD
    public class CineDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public double Latitud { get; set; }
		public double Longitud { get; set; }
	}
}
