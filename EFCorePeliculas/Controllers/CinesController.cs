using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCorePeliculas.Entidades;
using EFCorePeliculas.Entidades.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

//!=>Referencia [1] [Sección 03, 044. Consultando Datos Espaciales]
//!=>Referencia [2] [Sección 03, 045. Ordenando y Filtrando Datos Espaciales]

namespace EFCorePeliculas.Controllers
{
    [Route("api/cines")]
    [ApiController]
    public class CinesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CinesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //!=>[1] SIN EL ProjectTo RETORNA UN TIPO: Cine
        //public async Task<IEnumerable<Cine>> Get()
        public async Task<IEnumerable<CineDTO>> Get()
        {
            //!=>[1] MARCARÁ UN ERROR PORQUE TIENE DIFICULTADES PARA SERIALIZAR EL TIPO DE DATO Point EN LA ENTIDAD Cine
            //[1] return await context.Cines.ToListAsync();
            //!=>[1] PARA SOLUCIONAR EL ERROR ANTERIOR REALIZAMOS UN MAPEO QUE EXTRAE LA INFORMACIÓN NECESARIA DEL Point (Latitud y Longitud). ESTO SE CONFIGURA EN AutoMapperProfiles
            return await context.Cines.ProjectTo<CineDTO>(mapper.ConfigurationProvider).ToListAsync();
        }

        //!=>[2] RECUPERA LOS CINES A UNA DISTANCIA MENOR A 2 KM DE MÍ, ORDENADOS DEL MÁS CERCANO A MI UBICACIÓN
        [HttpGet("cercanos")]
        public async Task<ActionResult> Get(double latitud, double longitud)
        {
            //!=>[2] PERMITE REALIZAR MEDICIONES SOBRE NUESTRO PLANETA
            GeometryFactory geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            //!=>[2] SE CREA UNA VARIABLE QUE CONTIENE LA UBICACIÓN ACTUAL
            Point miUbicacion = geometryFactory.CreatePoint(new Coordinate(longitud, latitud));
            int distanciaMaximaEnMetros = 2000;
            // Listado de cines ordenados por el más cercano a mi ubicación actual
            var cines = await context.Cines
                //!=>[2] ORDENA LOS CINES POR EL MÁS CERCANO A MI UBICACIÓN
                .OrderBy(c => c.Ubicacion.Distance(miUbicacion))
                //!=>[2] IsWithinDistance() DEVUELVE EL REGISTRO QUE SU DISTANCIA SEA MENOR O IGUAL A LA INDICADA (distanciaMaximaEnMetros) CON RESPECTO A LA UBICACIÓN ESTABLECIDA (miUbicacion)
                .Where(c => c.Ubicacion.IsWithinDistance(miUbicacion, distanciaMaximaEnMetros))
                // Proyectamos el resultado a un tipo anónimo
                .Select(c => new
                {
                    Nombre = c.Nombre,
                    // Retorna la distancia en metros entre el registro seleccionado con respecto a la ubicación indicada (miUbicacion)
                    Distancia = Math.Round(c.Ubicacion.Distance(miUbicacion))
                }).ToListAsync();
            return Ok(cines);
        }
    }
}
