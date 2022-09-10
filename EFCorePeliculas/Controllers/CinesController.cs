using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCorePeliculas.Entidades;
using EFCorePeliculas.Entidades.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

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
		public async Task<IEnumerable<CineDTO>> Get()
		{
			return await context.Cines.ProjectTo<CineDTO>(mapper.ConfigurationProvider).ToListAsync();
		}

		[HttpGet("cercanos")]
		public async Task<ActionResult> Get(double latitud, double longitud)
		{
			// Permite realizar mediciones sobre nuestro planeta
			GeometryFactory geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
			// Creamos una variable con la ubicación actual
			Point miUbicacion = geometryFactory.CreatePoint(new Coordinate(longitud, latitud));
			int distanciaMaximaEnMetros = 2000;
			// Listado de cines ordenados por el más cercano a mi ubicación actual
			var cines = await context.Cines
				.OrderBy(c => c.Ubicacion.Distance(miUbicacion)) // Ordena por el más cercano a mi ubicación
				.Where(c => c.Ubicacion.IsWithinDistance(miUbicacion, distanciaMaximaEnMetros)) // Está dentro de la distancia (es menor o igual a la que se le pasará)
				.Select(c => new
				{
					Nombre = c.Nombre,
					Distancia = Math.Round(c.Ubicacion.Distance(miUbicacion)) // La distancia la obtiene en metros
				}).ToListAsync();
			return Ok(cines);
		}
	}
}
