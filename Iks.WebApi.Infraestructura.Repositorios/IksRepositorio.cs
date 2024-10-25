using Iks.WebApi.Dominio.DTOs;
using Iks.WebApi.Dominio.Interfaces;

namespace Iks.WebApi.Infraestructura.Repositorios;

public class IksRepositorio : IIksRepositorio
{
    public Task<bool> ActualizarIks(IksDto modelo)
    {
        throw new NotImplementedException();
    }

    public Task<bool> EliminarIks(long id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> GuardarIks(IksDto modelo)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<IksDto>> ObtenerConPaginacion(int numeroPagina, int tamañoPagina)
    {
        throw new NotImplementedException();
    }

    public Task<IksDto> ObtenerIks(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<IksDto>> ObtenerTodoIks()
    {
        throw new NotImplementedException();
    }
}
