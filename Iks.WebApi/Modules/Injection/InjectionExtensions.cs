using Iks.WebApi.Aplicacion.Interfaces;
using Iks.WebApi.Aplicacion.Servicios;
using Iks.WebApi.Dominio.Interfaces;
using Iks.WebApi.Dominio.Persistencia;
using Iks.WebApi.Infraestructura.Repositorios;
using Iks.WebApi.Transversal.Interfaces;
using Iks.WebApi.Transversal.Logging;

namespace Iks.WebApi.Modules.Injection;

public static class InjectionExtensions
{

    public static IServiceCollection AddInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);
        services.AddSingleton<DapperContext>();
        services.AddScoped<IIksRepositorio, IksRepositorio>();
        services.AddScoped<IIksServicio, IksServicio>();

        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        return services;
    }


}
