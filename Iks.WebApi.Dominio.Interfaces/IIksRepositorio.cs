using Iks.WebApi.Dominio.DTOs;

namespace Iks.WebApi.Dominio.Interfaces
{
    public interface IIksRepositorio
    {
        Task<bool> GuardarIks(IksDto modelo);
        Task<bool> ActualizarIks(IksDto modelo);
        Task<bool> EliminarIks(long id);
        Task<IksDto> ObtenerIks(long id);
        Task<IEnumerable<IksDto>> ObtenerTodoIks();
        Task<IEnumerable<IksDto>> ObtenerConPaginacion(int numeroPagina, int tamañoPagina);

    }
}
