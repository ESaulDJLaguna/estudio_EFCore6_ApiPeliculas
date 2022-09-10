using AutoMapper;
using EFCorePeliculas.Entidades;
using EFCorePeliculas.Entidades.DTOs;

namespace EFCorePeliculas.Servicios
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<Actor, ActorDTO>();

			CreateMap<Cine, CineDTO>()
				.ForMember(dto => dto.Latitud, ent => ent.MapFrom(prop => prop.Ubicacion.Y))
				.ForMember(dto => dto.Longitud, ent => ent.MapFrom(prop => prop.Ubicacion.X));

			CreateMap<Genero, GeneroDTO>();

			// Creamos un mapeo personalizado porque Pelicula NO tiene Cines, sino que tiene SalaDeCines
			CreateMap<Pelicula, PeliculaDTO>()
				// Tomamos Cine de la propiedad SalaDeCines de la entidad Pelicula y lo mapeamos a la propiedad Cines de PeliculaDTO
				.ForMember(pDto => pDto.Cines, entidad => entidad.MapFrom(p => p.SalasDeCines.Select(sc => sc.Cine)))
				// Tomamos Actor de la propiedad PeliculasACtores de la entidad Pelicula y lo mapeamos a la propiedad Actores del PeliculaDTO
				.ForMember(pDto => pDto.Actores, entidad => entidad.MapFrom(p => p.PeliculasActores.Select(pa => pa.Actor)));
		}
	}
}
