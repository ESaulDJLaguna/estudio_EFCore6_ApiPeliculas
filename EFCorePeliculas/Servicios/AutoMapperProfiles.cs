using AutoMapper;
using EFCorePeliculas.Entidades;
using EFCorePeliculas.Entidades.DTOs;

//!=>Referencia [1] [Sección 03, 043. Ahórrate el Select con AutoMapper]
//!=>Referencia [2] [Sección 03, 044. Consultando Datos Espaciales]
//!=>Referencia [3] [Sección 03, 047. Utilizando ThenInclude]
//!=>Referencia [4] [Sección 03, 049. ProjectTo y Eager Loading]

namespace EFCorePeliculas.Servicios
{
    //!=>[1] CLASE UTILIZADA PARA CONFIGURAR LOS MAPEOS AUTOMÁTICOS, SE REQUIERE HEREDAR DE Profile
    public class AutoMapperProfiles : Profile
	{
        //!=>[1] LOS MAPEOS SE CONFIGURAN DENTRO DEL CONSTRUCTOR: CreateMap<ClaseFuente, ClaseDestino>();
		public AutoMapperProfiles()
		{
            //!=>[1] CreateMap SE UTILIZA PARA REALIZAR LOS MAPEOS: CreateMap<ClaseFuente, ClaseDestino>()
            CreateMap<Actor, ActorDTO>();

            //!=>[2] QUEREMOS MAPAR UN Cine a CineDTO
            CreateMap<Cine, CineDTO>()
                //!=>[2] ForMember() PERMITE PERSONALIZAR LAS CONFIGURACIONES DE MIEMBROS INDIVIDUALES
                //!=>[2] MapFrom() INDICA DE QUÉ PROPIEDAD DE Cine QUEREMOS MAPEAR A Latitud
                // A LA PROPIEDAD Latitud DENTRO DE CineDTO MAPEALE EL VALOR DE LA PROPIEDAD Ubucacion.Y DENTRO DE Cine
                .ForMember(dto => dto.Latitud, entCine => entCine.MapFrom(prop => prop.Ubicacion.Y))
                // A LA PROPIEDAD Longitud DENTRO DE CineDTO MAPEALE EL VALOR DE LA PROPIEDAD Ubucacion.X DENTRO DE Cine
                .ForMember(dto => dto.Longitud, entCine => entCine.MapFrom(prop => prop.Ubicacion.X));

            CreateMap<Genero, GeneroDTO>();

            //!=>[3] MAPEO DE Pelicula A PeliculaDTO SIN ProjectTo (EAGEAR LOADING)
            //!=>[3] CREAMOS UN MAPEO PERSONALIZADO PORQUE Pelicula NO TIENE Cines, SINO QUE TIENE SalaDeCines
            CreateMap<Pelicula, PeliculaDTO>()
                //!=>[3] TOMAMOS Cine DE LA PROPIEDAD SalaDeCines DE LA ENTIDAD Pelicula Y LO MAPEAMOS A LA PROPIEDAD Cines DE PeliculaDTO
                .ForMember(pDto => pDto.Cines, entidad => entidad.MapFrom(p => p.SalasDeCines.Select(sc => sc.Cine)))
                //!=>[3] TOMAMOS Actor DE LA PROPIEDAD PeliculasActores DE LA ENTIDAD Pelicula Y LO MAPEAMOS A LA PROPIEDAD Actores DE PeliculaDTO
                .ForMember(pDto => pDto.Actores, entidad => entidad.MapFrom(p => p.PeliculasActores.Select(pa => pa.Actor)));

            //!=>[3] MAPEO DE Pelicula A PeliculaDTO CON ProjectTo (SE UTILIZA ESTE O EL ANTERIOR MAPEO, NO AMBOS)
            //CreateMap<Pelicula, PeliculaDTO>()
            //    //!=>[3] ORDENAMOS LOS GÉNEROS DE FORMA DESCENDENTE
            //    .ForMember(pDto => pDto.Generos, entidad => entidad.MapFrom(prop => prop.Generos.OrderByDescending(g => g.Nombre)))
            //    .ForMember(pDto => pDto.Cines, entidad => entidad.MapFrom(p => p.SalasDeCines.Select(sc => sc.Cine)))
            //    //!=>[3] FILTRAMOS LOS ACTORES POR SU AÑO DE NACIMIENTO (PRIMERO ES EL Where() Y EN SEGUIDA Select(), HACERLO AL REVÉS MARCARÁ ERROR
            //    .ForMember(pDto => pDto.Actores,
            //        entidad => entidad.MapFrom(p => p.PeliculasActores.Where(pa => pa.Actor.FechaNacimiento.Value.Year >= 1980).Select(pa => pa.Actor)));
        }
    }
}
