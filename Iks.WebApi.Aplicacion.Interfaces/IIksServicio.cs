﻿using Iks.WebApi.Dominio.DTOs;
using Iks.WebApi.Transversal.Modelos;

namespace Iks.WebApi.Aplicacion.Interfaces;

public interface IIksServicio
{
    Task<Response<IksDto>> ObtenerPorId(long id);
    Task<Response<IEnumerable<IksDto>>> ObtenerTodo();
    Task<Response<bool>> Guardar(IksDto modelo);
    Task<Response<bool>> Actualizar(long id, IksDto modelo);
    Task<Response<bool>> Eliminar(long id);
    Task<ResponsePagination<IEnumerable<IksDto>>> ObtenerTodoConPaginacion(int numeroPagina, int tamañoPagina);




}
