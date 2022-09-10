using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCorePeliculas.Entidades;
using EFCorePeliculas.Entidades.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculas.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PeliculasController : ControllerBase
	{
		private readonly ApplicationDbContext context;
		private readonly IMapper mapper;

		public PeliculasController(ApplicationDbContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<PeliculaDTO>> Get(int id)
		{
			var pelicula = await context.Peliculas
				.Include(p => p.Generos.OrderByDescending(g => g.Nombre))
				.Include(s => s.SalasDeCines)
					.ThenInclude(sc => sc.Cine)
				.Include(pa => pa.PeliculasActores.Where(a => a.Actor.FechaNacimiento.Value.Year >= 1980))
					.ThenInclude(a => a.Actor)
				.FirstOrDefaultAsync(p => p.Id == id);

			if(pelicula is null)
			{
				return NotFound();
			}

			var peliculaDto = mapper.Map<PeliculaDTO>(pelicula);

			peliculaDto.Cines = peliculaDto.Cines.DistinctBy(c => c.Id).ToList();

			return peliculaDto;
		}

		[HttpGet("conprojectto/{id:int}")]
		public async Task<ActionResult<PeliculaDTO>> GetProjectTo(int id)
		{
			var pelicula = await context.Peliculas
				.ProjectTo<PeliculaDTO>(mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(p => p.Id == id);

			if (pelicula is null)
			{
				return NotFound();
			}
			
			pelicula.Cines = pelicula.Cines.DistinctBy(c => c.Id).ToList();

			return pelicula;
		}
	}
}
