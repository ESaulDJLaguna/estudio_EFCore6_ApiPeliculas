using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCorePeliculas.Entidades;
using EFCorePeliculas.Entidades.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//!=>Referencia [1] [Sección 03, 042. Seleccionando Columnas con Select]
//!=>Referencia [2] [Sección 03, 043. Ahórrate el Select con AutoMapper]

namespace EFCorePeliculas.Controllers
{
	[Route("api/actores")]
	[ApiController]
	public class ActoresController : ControllerBase
	{
		private readonly ApplicationDbContext context;
		private readonly IMapper mapper;

        //!=>[2] UNA VEZ CONFIGURADO AutoMapper Y DEFINIDOS LOS MAPEOS, INYECTAMOS LA DEPENDENCIA
        public ActoresController(ApplicationDbContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

        //!=>[1] Select() REALIZA UNA PROYECCIÓN QUE PERMITE MAPEAR DE Actor A OTRO TIPO DE DATO
        //[HttpGet]
        //public async Task<ActionResult> Get()
        //{
        //!=>[1] MAPEO "MANUAL" DE LA ENTIDAD Actor A UN TIPO ANÓNIMO. NÓTESE EL TIPO DE RETORNO <ActionResult>
        //    var actores = await context.Actores.Select(a => new { Id = a.Id, Nombre = a.Nombre }).ToListAsync();
        //    return Ok(actores);
        //}

        //[1] [HttpGet]
        //public async Task<IEnumerable<ActorDTO>> Get()
        //{
        //!=>[1] MAPEO "MANUAL" DE LA ENTIDAD Actor A UNA CLASE ActorDTO. NÓTESE EL TIPO DE RETORNO IEnumerable<ActorDTO>
        //    return await context.Actores.Select(a => new ActorDTO { Id = a.Id, Nombre = a.Nombre }).ToListAsync();
        //}

        [HttpGet]
		public async Task<IEnumerable<ActorDTO>> Get()
		{
            //!=>[2] ProjectTo MAPEA LOS RESULTADOS OBTENIDOS DESDE LA CONSULTA DEL query AL TIPO ActorDTO
            return await context.Actores.ProjectTo<ActorDTO>(mapper.ConfigurationProvider).ToListAsync();
		}
	}
}
