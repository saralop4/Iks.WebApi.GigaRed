using Iks.WebApi.Aplicacion.Validadores;

namespace Iks.WebApi.Modules.Validator;


public static class ValidatorExtensions
{

    public static IServiceCollection AddValidator(this IServiceCollection services)
    {
        services.AddTransient<IksDtoValidador>();

        return services;
    }
}
