using Dapper;
using Iks.WebApi.Dominio.Interfaces;
using Iks.WebApi.Dominio.Persistencia;
using Iks.WebApi.Dominio.Persistencia.Modelos;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Iks.WebApi.Infraestructura.Repositorios;

public class IksRepositorio : IIksRepositorio
{
    private readonly DapperContext _dapperContext;

    public IksRepositorio(IConfiguration configuration)
    {
        _dapperContext = new DapperContext(configuration);  
    }
    public async Task<bool> Actualizar(Ik modelo)
    {
        using (var conexion = _dapperContext.CreateConnection())
        {
            var query = "ActualizarIks";
            var parameters = new DynamicParameters();
            parameters.Add("IdIks", modelo.IdIks);
          //  parameters.Add("IdCliente", modelo.IdCliente);
            parameters.Add("CodigoDeLlave", modelo.CodigoDeLlave);
            parameters.Add("UsuarioQueRegistra", modelo.UsuarioQueRegistra);
            parameters.Add("IpDeRegistro", modelo.IpDeRegistro);

            var result = await conexion.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            return result > 0;
        }
    }

    public async Task<bool> Eliminar(long id)
    {
        using (var conexion = _dapperContext.CreateConnection())
        {
            var query = "EliminarIks";
            var parameters = new DynamicParameters();
            parameters.Add("IdIks", id);

            var result = await conexion.ExecuteScalarAsync<int>(query, param: parameters, commandType: CommandType.StoredProcedure);

            return result > 0;
        }
    }

    public async Task<bool> Guardar(Ik modelo)
    {
        using(var conexion = _dapperContext.CreateConnection())
        {
            var query = "RegistrarIks";
            var parameters = new DynamicParameters();
            parameters.Add("IdCliente", modelo.IdCliente);
            parameters.Add("CodigoDeLlave", modelo.CodigoDeLlave);
            parameters.Add("UsuarioQueRegistra", modelo.UsuarioQueRegistra);
            parameters.Add("IpDeRegistro", modelo.IpDeRegistro);

            var result = await conexion.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            return result > 0;
        }
          
    }

    public async Task<IEnumerable<Ik>> ObtenerTodoConPaginacion(int numeroPagina, int tamañoPagina)
    {
        using (var connection = _dapperContext.CreateConnection()) 
        {
            var query = "ObtenerIksConPaginacion";
            var parameters = new DynamicParameters();

            parameters.Add("NumeroPagina", numeroPagina);
            parameters.Add("TamañoPagina", tamañoPagina);

            var result = await connection.QueryAsync<Ik>(query, param: parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
    }

    public async Task<Ik> ObtenerPorId(long id)
    {
        using (var conexion = _dapperContext.CreateConnection())
        {
            var query = "ObtenerIksPorId";
            var parameters = new DynamicParameters();
            parameters.Add("IdIks", id);

            var result = await conexion.QuerySingleOrDefaultAsync<Ik>(query, param: parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
    }

    public async Task<IEnumerable<Ik>> ObtenerTodo()
    {
        using (var connection = _dapperContext.CreateConnection())
        {
            var query = "ObtenerIks";

            var result = await connection.QueryAsync<Ik>(query, param: null, commandType: CommandType.StoredProcedure);
            return result;
        };
    }

    public async Task<int> Contar()
    {
        using(var connection = _dapperContext.CreateConnection())
        {
            var query = "select Count(*) from Iks"; 
            var parameters = new DynamicParameters();

            var result = await connection.ExecuteScalarAsync<int>(query, commandType: CommandType.Text);
            return result;
        };

    }
}
