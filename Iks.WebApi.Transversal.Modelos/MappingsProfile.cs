using AutoMapper;

namespace Iks.WebApi.Transversal.Modelos;

public class MappingsProfile : Profile
{
    public MappingsProfile() //esta clase se usa solo en el program
    {

        //de  esta forma se hace un mapeo de todos los campos porque tienen los mismos
        //el metodo ReverseMap() es para mapear de customers a customerDto y viceversa
        //  CreateMap<Usuario, UsuarioDto>().ReverseMap();
        // CreateMap<Users, UsersDto>().ReverseMap();

        //de esta forma se hace un mapeo por individual cuando las dos clases no tienen los mismos atributos,
        //DESTINO Y ORIGEN
        /*   CreateMap<Customers, CustomersDto>().ReverseMap()
               .ForMember(destination => destination.CustomerId, source => source.MapFrom(src =>src.CustomerId))
               .ForMember(destination => destination.CompanyName, source => source.MapFrom(src => src.CompanyName))
               .ForMember(destination => destination.ContactName, source => source.MapFrom(src => src.ContactName))
               .ForMember(destination => destination.ContactTitle, source => source.MapFrom(src => src.ContactTitle))
               .ForMember(destination => destination.Address, source => source.MapFrom(src => src.Address)).ReverseMap();
      */

    }
}
