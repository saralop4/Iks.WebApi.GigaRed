namespace Iks.WebApi.Modules.HealthChecks;

public static class HealthCheckExtensions
{
    public static IServiceCollection AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
    {

        //comprobaciones

        services.AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString("ConnectionStrings"), tags: new[] { "database" }) //dependencia del tipo database
            .AddCheck<HealthCheckCustom>("HealthCheckCustom", tags: new[] { "custom" }); //metodo personalizado

        services.AddHealthChecksUI().AddInMemoryStorage();

        return services;

    }

}
