namespace Iks.WebApi.Modules.Feature
{
    public static class FeatureExtensions
    {
        public static IServiceCollection AddFeature(this IServiceCollection services, IConfiguration configuration)
        {
            string MyPolicy = "policyApi"; // Política de CORS

            services.AddCors(options =>
            {
                options.AddPolicy(MyPolicy, corsBuilder =>
                {
                    if (configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT") == "Development")
                    {
                        // Permitir cualquier origen en desarrollo
                        corsBuilder.AllowAnyOrigin()
                                   .AllowAnyHeader()
                                   .AllowAnyMethod()
                                   .WithExposedHeaders("Content-Disposition");
                    }
                    else
                    {
                        var origins = configuration.GetSection("Config:OriginsCors").Get<string[]>();

                        corsBuilder.WithOrigins(origins)
                                   .AllowAnyHeader()
                                   .AllowAnyMethod()
                                   .WithExposedHeaders("Content-Disposition");
                    }
                });
            });

            services.AddMvc();
            return services;
        }
    }
}
