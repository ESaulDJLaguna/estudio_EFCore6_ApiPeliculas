using EFCorePeliculas.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//!=>Referencia [1] [Sección 03, 036. Un Simple Query]
//!=>Referencia [2] [Sección 03, 037. Queries Más Rápidos con AsNoTracking]
//!=>Referencia [3] [Sección 03, 038. Obteniendo el Primer Registro con First y FirstOrDefault]
//!=>Referencia [4] [Sección 03, 039. Filtrando con Where]
//!=>Referencia [5] [Sección 03, 040. Ordenando con OrderBy y OrderByDescending]
//!=>Referencia [6] [Sección 03, 041. Paginando con Skip y Take]

namespace EFCorePeliculas.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GenerosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        // Inyectamos el DbContext para acceder a mis tablas en mi base de datos
        public GenerosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Genero>> Get()
        {
            //!=>[2] QUERY DE SOLO LECTURA (MÁS RÁPIDO QUE UNO NORMAL), SE CREA UTIILZANDO AsNoTracking()
            //[2] return await context.Generos.AsNoTracking().ToListAsync();
            //!=>[2] SI CONFIGURAMOS QUERIES DE SOLO LECTURA DE MANERA GLOBAL (Program.cs) Y REQUERIMOS UN QUERY QUE TRAE INFORMACIÓN E INMEDIATAMENTE SE MODIFICARÁ O BORRARÁ, SOBREESCRIBIMOS EL COMPORTAMIENTO AsNoTracking() GLOBAL UTILIZANDO LA INSTRUCCIÓN AsTracking()
            //[2] return await context.Generos.AsTracking().ToListAsync();

            //!=>[1] QUERY QUE DEVUELVE UN LISTADO DE TODOS LOS GÉNEROS
            return await context.Generos
                //!=>[5] POR DEFECTO LOS REGISTROS SE ORDENAN POR Id: OrderBy() ORDENA DE LA A - Z; OrderByDescending() DE LA Z - A
                .OrderBy(g => g.Nombre)
                .ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Genero>> Get(int id)
        {
            var genero = await context.Generos.FirstOrDefaultAsync(g => g.Identificador == id);

            if (genero is null)
            {
                return NotFound();
            }

            return genero;
        }

        [HttpGet("primer")]
        public async Task<ActionResult<Genero>> Primer()
        {
            //!=>[3] RECUPERA EL PRIMER REGISTRO DE LA TABLA O EL PRIMERO QUE CUMPLA UNA CONDICIÓN, DE LO CONTRARIO, ARROJA UNA EXCEPCIÓN
            //[3] return await context.Generos.FirstAsync();
            //!=>[3] IGUAL QUE FirstAsync, LA DIFERENCIA ES QUE ARROJA null SI NO ENCUENTRA EL REGISTRO
            var genero = await context.Generos.FirstOrDefaultAsync(g => g.Nombre.StartsWith("z"));

            // Forma moderna de evaluar si una variable es NULL
            if (genero is null)
            {
                return NotFound();
            }

            return genero;
        }
        
        [HttpGet("filtrar")]
        public async Task<IEnumerable<Genero>> Filtrar(string nombre)
        {
            //!=>[4] Where(): FILTRA Y RECUPERA REGISTROS QUE CUMPLAN LA CONDICIÓN ESPECIFICADA
            //[4] return await context.Generos.Where(g => g.Nombre.StartsWith("C")).ToListAsync();
            //[4] return await context.Generos.Where(g => g.Nombre.StartsWith("C") || g.Nombre.StartsWith("A")).ToListAsync();
            //!=>[5] ORDENA REGISTROS DE FORMA ASCENDENTE. NO IMPORTA EL ORDEN DE OrderBy() y Where()
            //[5] return await context.Generos.OrderBy(g => g.Nombre).Where(g => g.Nombre.Contains(nombre)).ToListAsync();
            return await context.Generos
                .Where(g => g.Nombre.Contains(nombre))
                .ToListAsync();
        }

        [HttpGet("paginacion")]
        public async Task<ActionResult<IEnumerable<Genero>>> GetPaginacion(int pagina = 1)
        {
            int cantidadRegistrosPorPagina = 2;
            //!=>[6] CON EL USO DE SKIP Y TAKE SE PUEDE IMPLEMENTAR UNA PAGINACIÓN
            var generos = await context.Generos
                //!=>[6] Skip(): SALTA LOS n REGISTROS ESPECIFICADOS, COMIENZA A TOMAR DESDE EL n + 1
                .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                //!=>[6] Take(): RECUPERA LOS PRIMEROS n REGISTROS DE UNA TABLA
                .Take(cantidadRegistrosPorPagina)
                .ToListAsync();
            return generos;
        }
    }
}
