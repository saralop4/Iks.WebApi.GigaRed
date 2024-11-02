using Iks.WebApi.Modules.Authentication;
using Iks.WebApi.Modules.Feature;
using Iks.WebApi.Modules.Injection;
using Iks.WebApi.Modules.Mapper;
using Iks.WebApi.Modules.Swagger;
using Iks.WebApi.Modules.Validator;
using Iks.WebApi.Modules.Versioning;
using Iks.WebApi.Modules.WatchDog;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WatchDog;

namespace Iks.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        // Configurar la carga de la configuración según el entorno
        if (builder.Environment.IsDevelopment())
        {
            builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
        }
        else
        {
            builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        }

        // Add services to the container.

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver(); 
        });

        builder.Services.AddFeature(builder.Configuration);
        builder.Services.AddVersioning();
        builder.Services.AddMapper();
        builder.Services.AddAuthentication(builder.Configuration);
        builder.Services.AddValidator();
        builder.Services.AddInjection(builder.Configuration);
        builder.Services.AddSwaggerDocumentation();
        builder.Services.AddWatchDog(builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });
        }

        app.Use(async (context, next) =>
        {
            try
            {
                // Primero llamas al siguiente middleware en la cadena
                await next();
            }
            catch (DbUpdateException ex)
            {
                context.Response.StatusCode = 400;
                var result = JsonSerializer.Serialize(new
                {
                    Mensaje = ex.Message
                });
                await context.Response.WriteAsync(result);
            }

            catch (Exception ex)
            {
                // Manejar excepciones aquí y devolver una respuesta JSON
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var result = JsonSerializer.Serialize(new
                {
                    Mensaje = $"Ah ocurrido un error inesperado en el servidor, por favor contactar al administrador del sistema. ({ex.Message})"
                });

                await context.Response.WriteAsync(result);
            }

            // Luego verificas el código de estado de la respuesta
            if (context.Response.StatusCode == 401)
            {
                var result = JsonSerializer.Serialize(new { Mensaje = "No se ha autenticado para realizar este proceso." });
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }
            else if (context.Response.StatusCode == 403)
            {
                var result = JsonSerializer.Serialize(new { Mensaje = "No tienes permiso para realizar esta acción." });
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }
        });

        app.UseHttpsRedirection();
        app.UseCors("policyApi");
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.UseWatchDog(opt =>
        {
            opt.WatchPageUsername = builder.Configuration["WatchDog:WatchPageUsername"];
            opt.WatchPagePassword = builder.Configuration["WatchDog:WatchPagePassword"];
            //opt.WatchPageUsername = "admin";
            //opt.WatchPagePassword = "admin@123";
        });


        app.Run();
    }
}
