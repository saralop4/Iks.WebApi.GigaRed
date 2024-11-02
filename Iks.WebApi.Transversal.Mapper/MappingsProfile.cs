using AutoMapper;
using Iks.WebApi.Dominio.DTOs;
using Iks.WebApi.Dominio.Persistencia.Modelos;

namespace Iks.WebApi.Transversal.Mapper
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<Ik, IksDto>()
                .ForMember(dest => dest.IdIks, opt => opt.MapFrom(src => src.IdIks))
                .ForMember(dest => dest.IdCliente, opt => opt.MapFrom(src => src.IdCliente))
                .ForMember(dest => dest.CodigoDeLlave, opt => opt.MapFrom(src => src.CodigoDeLlave))
                .ForMember(dest => dest.EstadoEliminado, opt => opt.MapFrom(src => src.EstadoEliminado))
                .ForMember(dest => dest.UsuarioQueRegistra, opt => opt.MapFrom(src => src.UsuarioQueRegistra))
                .ForMember(dest => dest.UsuarioQueActualiza, opt => opt.MapFrom(src => src.UsuarioQueActualiza))
                .ForMember(dest => dest.IpDeRegistro, opt => opt.MapFrom(src => src.IpDeRegistro))
                .ForMember(dest => dest.FechaDeActualizado, opt => opt.MapFrom(src => src.FechaDeActualizado.HasValue
                    ? (DateTime?)new DateTime(src.FechaDeActualizado.Value.Year, src.FechaDeActualizado.Value.Month, src.FechaDeActualizado.Value.Day)
                    : null))
                .ForMember(dest => dest.HoraDeActualizado, opt => opt.MapFrom(src => src.HoraDeActualizado.HasValue
                    ? (TimeSpan?)new TimeSpan(src.HoraDeActualizado.Value.Hour, src.HoraDeActualizado.Value.Minute, src.HoraDeActualizado.Value.Second)
                    : null))
                .ForMember(dest => dest.IpDeActualizado, opt => opt.MapFrom(src => src.IpDeActualizado))
                .ReverseMap()
                .ForMember(dest => dest.FechaDeActualizado, opt => opt.MapFrom(src => src.FechaDeActualizado.HasValue
                    ? DateOnly.FromDateTime(src.FechaDeActualizado.Value)
                    : (DateOnly?)null))
                .ForMember(dest => dest.HoraDeActualizado, opt => opt.MapFrom(src => src.HoraDeActualizado.HasValue
                    ? TimeOnly.FromTimeSpan(src.HoraDeActualizado.Value)
                    : (TimeOnly?)null))
                .ForMember(dest => dest.FechaDeRegistro, opt => opt.Ignore())
                .ForMember(dest => dest.HoraDeRegistro, opt => opt.Ignore())
                .ForMember(dest => dest.IdClienteNavigation, opt => opt.Ignore());
        }
    }
}
