using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace Iks.WebApi.Modules.Swagger;


public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>(); //inyectamos la clase ConfigureSwaggerOptions

        services.AddSwaggerGen(c =>
        {

            var securityScheme = new OpenApiSecurityScheme
            {
                Description = "Ingrese el token JWT **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Name = "Authorization",
                Scheme = "bearer", //token de portador
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            };

            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);


            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {securityScheme, new List<string>()}
            });
        });

        return services;
    }
}
