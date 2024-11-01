using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Iks.WebApi.Modules.HealthChecks;

//HealthCheck Personalizados
public class HealthCheckCustom : IHealthCheck
{
    private readonly Random _random = new();
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        //esto no se debe aplicar en la vida real ya que es algo fictisio 
        var responseTime = _random.Next(1, 300); //simulando con un valor aleatorio el tiempo de respuesta de las diversas dependencias

        if (responseTime < 100)
        {
            return Task.FromResult(HealthCheckResult.Healthy("Healthy result from HealthCheckCustom"));

        }
        else if (responseTime < 200)
        {
            return Task.FromResult(HealthCheckResult.Degraded("Degraded result from HealthCheckCustom"));
        }
        return Task.FromResult(HealthCheckResult.Unhealthy("Unhealthy result from HealthCheckCustom"));

    }
}
