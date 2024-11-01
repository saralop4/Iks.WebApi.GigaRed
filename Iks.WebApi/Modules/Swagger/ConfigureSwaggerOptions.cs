using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Iks.WebApi.Modules.Swagger;


public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{

    readonly IApiVersionDescriptionProvider provider; // esta interfaz nos permite mostrar todas las versiones que utiliza la api

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
    /// </summary>
    /// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

    /// <inheritdoc />
    public void Configure(SwaggerGenOptions options)
    {

        foreach (var description in provider.ApiVersionDescriptions)
        {
            if (!options.SwaggerGeneratorOptions.SwaggerDocs.ContainsKey(description.GroupName))
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }
    }
    static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Version = description.ApiVersion.ToString(),
            Title = "Microservicio",
            Description = "Microservicio Empresarial Web Api",
            TermsOfService = new Uri("https://example.com/terms"),
            Contact = new OpenApiContact
            {
                Name = "Sarah Lopez",
                Email = "saralopezjb16@gmail.com",
                Url = new Uri("https://Ecommerce.com/contact")
            },
            License = new OpenApiLicense
            {
                Name = "Giga Red Connection Col",
                Url = new Uri("https://example.com/license")
            }
        };

        if (description.IsDeprecated)
        {
            info.Description += "Esta version de la API ha quedado obsoleta"; // esta parte de simplementa para informar que version está deprecada
        }
        return info;

    }


}
