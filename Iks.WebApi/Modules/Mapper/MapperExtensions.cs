using AutoMapper;
using Iks.WebApi.Transversal.Mapper;

namespace Iks.WebApi.Modules.Mapper;

public static class MapperExtensions
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {

        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingsProfile());
        });

        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);


        return services;
    }

}
