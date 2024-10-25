using Dapper;
using Iks.WebApi.Dominio.DTOs;
using Iks.WebApi.Dominio.Interfaces;
using Iks.WebApi.Dominio.Persistencia;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Reflection;

namespace Iks.WebApi.Infraestructura.Repositorios;

public class IksRepositorio : IIksRepositorio
{
    private readonly DapperContext _dapperContext;

    public IksRepositorio(IConfiguration configuration)
    {
        _dapperContext = new DapperContext(configuration);  
    }
    public async Task<bool> Actualizar(IksDto modelo)
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

    public Task<bool> Eliminar(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Guardar(IksDto modelo)
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

    public Task<IEnumerable<IksDto>> ObtenerTodoConPaginacion(int numeroPagina, int tamañoPagina)
    {
        throw new NotImplementedException();
    }

    public async Task<IksDto> ObtenerPorId(long id)
    {
        using (var conexion = _dapperContext.CreateConnection())
        {
            var query = "ObtenerIks";
            var parameters = new DynamicParameters();
            parameters.Add("IdIks", id);

            var result = await conexion.QuerySingleAsync<IksDto>(query, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }
    }

    public Task<IEnumerable<IksDto>> ObtenerTodo()
    {
        throw new NotImplementedException();
    }

    public Task<int> Contar()
    {
        throw new NotImplementedException();
    }
}
