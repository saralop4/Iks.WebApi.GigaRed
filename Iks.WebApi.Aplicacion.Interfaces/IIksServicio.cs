using Iks.WebApi.Dominio.DTOs;
using Iks.WebApi.Transversal.Modelos;

namespace Iks.WebApi.Aplicacion.Interfaces;

public interface IIksServicio
{
    Task<Response<IksDto>> Obtener(long id);
    Task<Response<IEnumerable<IksDto>>> ObtenerTodo();
    Task<Response<bool>> Guardar(IksDto modelo);
    Task<Response<bool>> Actualizar(long id, IksDto modelo);
    Task<Response<bool>> Eliminar(long id);
    Task<Response<IEnumerable<IksDto>>> ObtenerTodoConPaginacion(int numeroPagina, int tamañoPagina);
    Task<Response<int>> Contar();



}
