namespace Iks.WebApi.Dominio.Interfaces;

public interface IGenericRepository<T> where T: class //agregamos una restriccion para que T siempre sea de tipo class
{
    #region Metodos Asincronos
    Task<bool> Guardar(T modelo);
    Task<bool> Actualizar(T modelo);
    Task<bool> Eliminar(long id);
    Task<T> ObtenerPorId(long id);
    Task<IEnumerable<T>> ObtenerTodo();
    Task<IEnumerable<T>> ObtenerTodoConPaginacion(int numeroPagina, int tamañoPagina);
    Task<int> Contar();

    #endregion

}
