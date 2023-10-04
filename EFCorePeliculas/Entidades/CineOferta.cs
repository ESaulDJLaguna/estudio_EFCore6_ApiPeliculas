//!=>Referencia [1] [Sección 02, 024. Relaciones Uno a Uno - Propiedades de Navegación - Ofertas de Cines]

namespace EFCorePeliculas.Entidades
{
    //!=>[1] RELACIÓN (1:1). 1 Cine PUEDE TENER 1 OFERTA (O NINGUNA), Cine NUNCA TENDRÁ DOS O MÁS OFERTAS
    public class CineOferta
	{
		public int Id { get; set; }
		public DateTime FechaInicio { get; set; }
		public DateTime FechaFin { get; set; }
		public decimal PorcentajeDescuento { get; set; }
        //!=>[1] A QUÉ CINE LE VA A CORRESPONDER UNA OFERTA
        public int CineId { get; set; }
	}
}
