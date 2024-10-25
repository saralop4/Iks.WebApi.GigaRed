using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Iks.WebApi.Dominio.Persistencia;

public class DapperContext
{
    private readonly string _connectionString;
    private readonly IConfiguration _configuration;
    private IDbConnection? _connection;
    private bool _disposed = false; // Para evitar liberar varias veces

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = configuration.GetConnectionString("Dev")
                            ?? throw new ArgumentNullException(nameof(_connectionString), "La cadena de conexión no puede ser nula o vacía.");
    }

    public IDbConnection CreateConnection()
    {
        if (_connection == null || _connection.State == ConnectionState.Closed)
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
        }
        return _connection;
    }

    // Implementación del patrón Dispose
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Liberar recursos administrados
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                    _connection.Dispose();
                }
            }

            // Aquí se puede liberar recursos no administrados si los hubiera

            _disposed = true;
        }
    }

    ~DapperContext()
    {
        Dispose(false);
    }
}