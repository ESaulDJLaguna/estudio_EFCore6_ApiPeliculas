using AutoMapper;
using AutoMapper.QueryableExtensions;
using Castle.Core.Internal;
using EFCorePeliculas.Entidades;
using EFCorePeliculas.Entidades.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//!=>Referencia [1] [Sección 03, 046. Eager Loading - Include - Cargando Data Relacionada]
//!=>Referencia [2] [Sección 03, 047. Utilizando ThenInclude]
//!=>Referencia [3] [Sección 03, 048.Ordenando y Filtrando la Data Relacionada]
//!=>Referencia [4] [Sección 03, 049. ProjectTo y Eager Loading]
//!=>Referencia [5] [Sección 03, 050. Select Loading - Cargando Selectivo]
//!=>Referencia [6] [Sección 03, 051. Explicit Loading - Carga Explícita]
//!=>Referencia [7] [Sección 03, 052. Lazy Loading - Carga Perezosa]
//!=>Referencia [8] [Sección 03, 054. GroupBy]
//!=>Referencia [9] [Sección 03, 055. Ejecución Diferida]

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
            //!=>[1] TÉCNICA 'EAGER LOADING': INDICA EXPLÍCITAMENTE QUE QUEREMOS CARGAR DATOS RELACIONADOS
            //var pelicula = await context.Peliculas
            //!=>[1] EL PARÁMETRO DE 'Include()' ES LA PROPIEDAD DE NAVEGACIÓN QUE CONTIENE LA INFORMACIÓN RELACIONADA
            //!=>[1] ERROR: System.Text.Json.JsonException: A possible object cycle was detected. PORQUE LA ENTIDAD Pelicula TIENE UNA REFERENCIA A Genero Y Genero TIENE UNA REFERENCIA A Pelicula; GENERA UN CICLO INFINITO
			//!=>[1] DOS POSIBLES SOLUCIONES: CON UN DTO O MODIFICANDO Program.cs (SE HARÁ ESTO)
            //.Include(p => p.Generos).FirstOrDefaultAsync(p => p.Id == id);


            var pelicula = await context.Peliculas
                .Include(p => p.Generos
                //!=>[3] POR DEFECTO, ORDENA LOS Generos POR Identificador, PERO PODEMOS INDICAR QUE LO HAGA EN ORDEN ALFABÉTICO. DEBERÁ CAMBIARSE EL HashSet<Genero> A List<Genero> DENTRO DE LA ENTIODAD Pelicula
                .OrderByDescending(g => g.Nombre))
                //!=>[2] SI ADEMÁS DE LOS GÉNEROS, QUEREMOS TODA LA INFORMACIÓN DE LAS SALAS DE CINE, USAMOS UN SEGUNDO Include(), CON SU RESPECTIVA PROPIEDAD DE NAVEGACIÓN
				.Include(s => s.SalasDeCines)
				//!=> [2] SI SOLO QUIERO LA INFORMACIÓN DEL CINE DÓNDE SE PROYECTARÁ LA PELÍCULA, CON ThenInclude() PODREMOS ACCEDER A LAS PROPIEDADES DE SalasDeCines DENTRO DE LA CUAL ESTÁ LA PROPIEDAD DE NAVEGACIÓN HACIA LA INFORMACIÓN DEL Cine
					.ThenInclude(sc => sc.Cine)
					//!=>[3] ES POSIBLE FILTRAR LOS DATOS RELACIONADOS
				.Include(pa => pa.PeliculasActores.Where(a => a.Actor.FechaNacimiento.Value.Year >= 1980))
					.ThenInclude(a => a.Actor)
				.FirstOrDefaultAsync(p => p.Id == id);

			if(pelicula is null)
			{
				return NotFound();
			}

            //!=> [2] UNA VEZ RECUPERADA LA INFORMACIÓN DE Peliculas NOS MARCARÁ EL ERROR: System.ArgumentException: .NET number values such as positive and negative infinity cannot be written as valid JSON, PORQUE ESTAMOS INTENTANDO SERIALIZAR LA PROPIEDAD DE TIPO Point DE LA ENTIDAD Cine. LA SOLUCIÓN ES CON LA CREACIÓN DE 2 DTO: PeliculaDTO y GeneroDTO, DICHOS MAPEOS DEBERÁN CONFIGURARSE EN AutoMapperProfiles. ADEMÁS DE UTILIZAR ProjectTo() PARA REALIZAR LOS MAPEOS, TAMBIÉN SE PUEDE UTILIZAR MAP<>()
            var peliculaDto = mapper.Map<PeliculaDTO>(pelicula);
            //!=> [2] SI NO SE INDICA QUE NOS TRAIGA SOLO LOS Cines DISTINTOS, SE REPETIRÁN 3 VECES
            peliculaDto.Cines = peliculaDto.Cines.DistinctBy(c => c.Id).ToList();

			return peliculaDto;
		}

        //!=> [4] CUANDO SE TRABAJA CON ProjectTo() LOS DATOS RELACIONADA SE MANDEJA DE MANERA DIFERENTE. ESTE ENDPOINT HACE LO MISMO QUE EL ANTERIOR (SIN EL ORDENADO NI FILTRADO, ESO SE HACE EN AutoMapperProfiles.cs) PERO SIN LA NECESIDAD DE TANTOS Include() y ThenInclude(): DEVUELVE LA INFORMACIÓN DE LA PELÍCULA JUNTO CON SU DATA RELACIONADA
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

        //!=> [5] Select(), ADEMÁS DE SELECCIONAR COLUMNAS ESPECÍFICAS, SE USA PARA CARGAR DATA RELACIONADA
        [HttpGet("cargadoselectivo/{id:int}")]
		public async Task<ActionResult> GetSelectivo(int id)
		{
            //[5] En el Select() se genera una proyección de tipo anónimo porque no es un tipo de dato concreto
            var pelicula = await context.Peliculas.Select(p =>
			new
			{
				Id = p.Id,
				Titulo = p.Titulo,
				Generos = p.Generos.OrderByDescending(g => g.Nombre)
                    //!=> [5] SIN EL Select() CARGARÁ TODA LA INFORMACIÓN DE LOS GÉNEROS
					.Select(g => g.Nombre).ToList(),
				CantidadActores = p.PeliculasActores.Count(),
				CantidadCines = p.SalasDeCines.Select(s => s.CineId).Distinct().Count()
			}).FirstOrDefaultAsync(p => p.Id == id);

			if(pelicula is null)
			{
				return NotFound();
			}

			return Ok(pelicula);
		}

        //!=> [6] EXPLICIT LOADING: PRIMERO CARGAMOS PELÍCULA Y DESPUÉS: Actores, Cines, Generos. NO ES UNA TÉCNICA MUY EFICIENTE
        [HttpGet("cargadoexplicito/{id:int}")]
        public async Task<ActionResult<PeliculaDTO>> GetExplicito(int id)
        {
            //!=> [6] DE MANERA GLOBAL CONFIGURAMOS QUERIES DE SOLO LECTURA, ANULAMOS ESO CON AsTracking(). ESTE QUERY NO CARGA DATOS RELACIONADOS
            var pelicula = await context.Peliculas.AsTracking().FirstOrDefaultAsync(p => p.Id == id);

            if (pelicula is null)
            {
                return NotFound();
            }

            //!=> [6] LoadAsync() SE ENCARGA DE CARGAR LOS Generos: ACTUALIZA LA VARIABLE pelicula POR LO TANTO, AHORA SÍ TIENE INFORMACIÓN RELACIONADA (Generos)
            //await context.Entry(pelicula).Collection(p => p.Generos).LoadAsync();

            //!=> [6] NO ESTAMOS LIMITADOS A CARGAR LOS DATOS RELACIONADOS, PODEMOS REALIZAR QUERIES SOBRE ELLA SI ES NECESARIO
            var cantidadGeneros = await context.Entry(pelicula).Collection(p => p.Generos).Query().CountAsync();

            var peliculaDTO = mapper.Map<PeliculaDTO>(pelicula);

            return peliculaDTO;
        }

        //!=> [7] CARGADO DE DATOS POR MEDIO DE LAZY LOADING
        [HttpGet("lazyloading/{id:int}")]
		public async Task<ActionResult<PeliculaDTO>> GetLazyLoading(int id)
		{
			//! RECORDEMOS QUE GLOBALMENTE ESTÁN CONFIGURADOS QUERIES DE SOLO LECTURA, COMO MODIFICAREMOS LA CONSULTA, REVERTIMOS CON AsTracking()
			var pelicula = await context.Peliculas.AsTracking().FirstOrDefaultAsync(p => p.Id == id);

			if (pelicula is null)
			{
				return NotFound();
			}

			//!=> [7] CUANDO SE HACE EL MAPEO, AUTOMAPPER INTENTA ACCEDER A LA INFORMACIÓN, POR EJEMPLO, DE Genero (EN Pelicula) PARA MAPEARLO A GeneroDTO (EN PeliculaDTO), POR LO TANTO, AL NO EXISTIR LA INOFORMACIÓN, SE CARGA GRACIAS A LAZY LOADING.
			var peliculaDTO = mapper.Map<PeliculaDTO>(pelicula);
            peliculaDTO.Cines = peliculaDTO.Cines.DistinctBy(x => x.Id).ToList();
			return peliculaDTO;
		}

        //!=> [7] LAZY LOADING: CONTIENE EL PROBLEMA N + 1
        [HttpGet("lazyloadingall")]
		public async Task<ActionResult<List<PeliculaDTO>>> GetLazyLoading()
		{
			// Ejecuta un query que recupera todas las películas
			var peliculas = await context.Peliculas.AsTracking().ToListAsync();

            foreach (var pelicula in peliculas)
            {
                // Carga los géneros de la película
                //!=> [7] PROBLEMA n + 1: POR CADA REGISTRO DENTRO DE peliculas, CARGAREMOS SUS RESPECTIVOS GENEROS (REALIZA UN QUERY EN CADA CICLO PARA BUSCAR SUS GÉNEROS).
                pelicula.Generos.ToList();
            }

			var peliculasDTOs = mapper.Map<List<PeliculaDTO>>(peliculas);
			return peliculasDTOs;
        }

		[HttpGet("agrupadasPorEstreno")]
		public async Task<ActionResult> GetAgrupadasPorCartelera()
		{
            //!=> [8] GroupBy(): QUIERE DECIR QUE AQUELLOS REGISTROS QUE TENGAN EL MISMO VALOR EN EL CAMPO EnCartelera, SERÁN AGRUPADOS EN UN MISMO LUGAR
            var peliculasAgrupadas = await context.Peliculas.GroupBy(p => p.EnCartelera)
                //!=> [8] OBLIGATORIAMENTE DEBE HACERSE UN Select()
                .Select(g => new
				{
                    //!=> [8] Key SE REFIERE AL VALOR QUE SE UTILIZÓ PARA AGRUPAR, EN ESTE CASO EnCartelera
                    EnCartelera = g.Key,
                    //!=> [8] CONTEO DE CUÁNTAS PELÍCULAS SE ENCUENTRAN EN CADA GRUPO
                    Conteo = g.Count(),
                    //!=> [8] RECUPERA EL LISTADO DE Peliculas DE CADA GRUPO (EnCartelera : true | false)
                    Peliculas = g.ToList()
				}).ToListAsync();

			return Ok(peliculasAgrupadas);
		}

        [HttpGet("agrupadasPorCantidadDeGeneros")]
        public async Task<ActionResult> GetAgrupadasPorCantidadDeGeneros()
        {
            //!=> [8] GroupBy(): SE AGRUPARÁ POR CANTIDAD DE GÉNEROS, POR ESO SE UTILIZA p.Generos.Count()
            var peliculasAgrupadas = await context.Peliculas.GroupBy(p => p.Generos.Count())
                .Select(g => new
                {
                    //!=> [8] RECORDEMOS QUE .Key ES LO QUE SE PASÓ COMO ARGUMENTO A GroupBy(), EN ESTE CASO SERÁ LA CANTIDAD DE GÉNEROS
                    Conteo = g.Key,
                    //!=> [8] RECUPERAMOS EL TÍTULO DE LA PELÍCULA
                    Titulos = g.Select(x => x.Titulo),
                    //!=> [8] SIN EL SelectMany() RECUPERARÁ LOS GÉNEROS DE CADA PELÍCULA OBTENIDA, POR LO QUE LA INFORMACIÓN SE REPETIRÁ N VECES, ES DECIR, SERÁ UN ARREGLO QUE CADA ELEMENTO SEA UN ARREGLO DE LOS GÉNEROS DE CADA PELÍCULA (UN ARREGLO DE ARREGLOS)
                    Generos = g.Select(p => p.Generos)
                    //!=> [8] SelectMany() "APLANA" LAS COLECCIONES DE Generos Y MANTIENE UNA SOLA COLECCIÓN CON LOS Generos, (gen => gen), LO QUE HACE ES TOMAR LAS COLECCIONES INTERNAS Y COLACARLOS EN UNA SOLA COLECCIÓN; ES DECIR, EL ARREGLO DE ARREGLOS QUE SE TENÍA ANTERIORMENTE, AHORA SE CONVIERTE EN UN ARREGLO DE OBJETOS, AUNQUE LOS Géneros SE SEGUIRÁN REPITIENDO TANTAS PELÍCULAS SE HAYAN RECUPERADO
                    .SelectMany(gen => gen)
                    //!=> [8] CON Select() RECUPERAMOS SOLO LOS NOMBRES DE LOS GÉNEROS Y CON Distinct() EVITAMOS QUE SE RECUPEREN N VECES LA MISMA INFORMACIÓN
                    .Select(gen => gen.Nombre).Distinct()
                }).ToListAsync();

            return Ok(peliculasAgrupadas);
        }

        //!=> [9] [FromQuery] SE UTILIZA PORQUE SE ESTÁ HACIENDO UN HttpGet Y SE ESTÁ RECIBIENDO UN TIPO DE DATO COMPLEJO, POR LO TANTO FromQuery ME PERMITIRÁ RECIBIR LOS DATOS DE PeliculasFiltroDTO DESDE QUERY STRINGS
        [HttpGet("filtrar")]
		public async Task<ActionResult<List<PeliculaDTO>>> Filtrar([FromQuery] PeliculasFiltroDTO peliculasFiltroDTO)
		{
            //!=> [9] EJECUCIÓN DIFERIDA: Queryable, BÁSICAMENTE ES EL TIPO DE DATO NOS PERMITE IR CONSTRUYENDO NUESTROS QUERIES, ASÍ, A TRAVÉS DE LA VARIABLE peliculasQueryable, SE IRÁ ARMANDO EL QUERY PASO POR PASO Y SOLO EJECUTARLO AL FINAL UNA SOLA VEZ
            var peliculasQueryable = context.Peliculas.AsQueryable();

            //!=> [9] SOLO SE APLICARÁN LOS FILTROS QUE SEAN ENVIADOS POR EL CLIENTE
            if (!string.IsNullOrEmpty(peliculasFiltroDTO.Titulo))
            {
                peliculasQueryable = peliculasQueryable.Where(p => p.Titulo.Contains(peliculasFiltroDTO.Titulo));
            }

            if(peliculasFiltroDTO.EnCartelera)
            {
                peliculasQueryable = peliculasQueryable.Where(p => p.EnCartelera);
            }

            if(peliculasFiltroDTO.ProximosEstrenos)
            {
                var today = DateTime.Today; // Recupera fecha actual la hora inicia a las 0:00:00 h
                var now = DateTime.Now; // Recupera fecha y hora actual
                peliculasQueryable = peliculasQueryable.Where(p => p.FechaEstreno > today);
            }

            //!=> [9] EL FILTRO DE GeneroId ES ESPECIAL PORQUE SERÁ UN FILTRO POR DATA RELACIONADA, YA QUE EL GÉNERO NO SE ENCUENTRA EN LA TABLA DE Peliculas SINO EN LA DE PeliculasGeneros
            if(peliculasFiltroDTO.GeneroId != 0)
            {
                peliculasQueryable = peliculasQueryable.Where(p =>
                    p.Generos.Select(g => g.Identificador)
                        .Contains(peliculasFiltroDTO.GeneroId));
            }

            //!=> [9] ANTES DE REALIZAR EL MAPEO, ES NECESARIO EJECUTAR EL QUERY PRIMERO. SE USA .Include(p => p.Generos) PARA DEMOSTRAR QUE SE PUEDEN HACER Includes() EN QUERIES DIFERIDOS. [RECORDEMOS QUE Include() NOS TRAE LA DATA RELACIONADA, SIN ESTE, LOS Generos SERÍAN UN ARREGLO VACÍO]
            var peliculas = await peliculasQueryable.Include(p => p.Generos).ToListAsync();

            return mapper.Map<List<PeliculaDTO>>(peliculas);
        }
    }
}
