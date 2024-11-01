using Microsoft.AspNetCore.Mvc.Versioning;

namespace Iks.WebApi.Modules.Versioning;

public static class VersioningExtensions
{
    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(o =>
        {
            o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0); 
            o.AssumeDefaultVersionWhenUnspecified = true; 
            o.ReportApiVersions = true; 
            o.ApiVersionReader = new UrlSegmentApiVersionReader(); 
        });
        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true; //esta propiedad nos sirve para indicar que se va a reemplazar un segmento de url por la version de la api

        });
        return services;

    }

}
