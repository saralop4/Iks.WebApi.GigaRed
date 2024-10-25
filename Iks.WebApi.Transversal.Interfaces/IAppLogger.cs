namespace Iks.WebApi.Transversal.Interfaces;

public interface IAppLogger<T>
{
    //metodos que permiten registrar errores
    void LogInformation(string message, params object[] args);
    void LogWarning(string message, params object[] args);
    void LogError(string message, params object[] args);

}
