using Iks.WebApi.Transversal.Interfaces;
using Microsoft.Extensions.Logging; 

namespace Iks.WebApi.Transversal.Modelos;

public class LoggerAdapter<T> : IAppLogger<T>
{
    private readonly ILogger<T> _logger;
    public LoggerAdapter(ILoggerFactory loggerFactory) //esta interfaz nos permite crear los sistemas de loggin y trazabilidad de las excepciones
    {
        _logger = loggerFactory.CreateLogger<T>();
    }
    public void LogInformation(string message, params object[] args)
    {
        _logger.LogInformation(message, args);  //estos metodos son propios del componente nugget que se instaló

    }
    public void LogWarning(string message, params object[] args)
    {
        _logger.LogWarning(message, args);

    }
    public void LogError(string message, params object[] args)
    {
        _logger.LogError(message, args);

    }
}
