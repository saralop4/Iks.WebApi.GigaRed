using Iks.WebApi.Aplicacion.Interfaces;
using Iks.WebApi.Aplicacion.Validadores;
using Iks.WebApi.Dominio.DTOs;
using Iks.WebApi.Dominio.Interfaces;
using Iks.WebApi.Transversal.Interfaces;
using Iks.WebApi.Transversal.Modelos;

namespace Iks.WebApi.Aplicacion.Servicios;

public class IksServicio : IIksServicio
{
    private readonly IIksRepositorio _iksRepositorio;
    private readonly IksDtoValidador _iksDtoValidador;
    private readonly IAppLogger<IksServicio> _logger;

    public IksServicio(IIksRepositorio iksRepositorio, IksDtoValidador iksDtoValidador, IAppLogger<IksServicio> logger)
    {
        _iksDtoValidador = iksDtoValidador;
        _iksRepositorio = iksRepositorio;
        _logger = logger;
    }

    public async Task<Response<bool>> Actualizar(long id, IksDto modelo)
    {
        var response = new Response<bool>();


        var validation = _iksDtoValidador.Validate(new IksDto()
        {
            CodigoDeLlave = modelo.CodigoDeLlave,
            UsuarioQueActualiza = modelo.UsuarioQueActualiza,
            IpDeActualizado= modelo.IpDeActualizado

        });

        if (!validation.IsValid)
        {
            response.IsSuccess = false;
            response.Message = "Errores de validación encontrados";
            response.Errors = validation.Errors;
            return response;

        }

        if (id == 0)
        {
            response.IsSuccess = false;
            response.Message = "Debe proporcionar el id del iks a actualizar.";
            _logger.LogWarning("Hubo un error al no proporcionar el id del registro que se va a actualizar");
            return response;
        }
        try
        {
            var iksExitente = Obtener(id);

            if (iksExitente is null)
            {
                response.IsSuccess = false;
                response.Message = "El iks a actualizar no existe";
                _logger.LogWarning("No existe  el registro que se intenta consultar, ya que viene vacia la consulta");
                return response;

            }

            var iks = await _iksRepositorio.Actualizar(modelo);

            if (iks is { }) // no es nulo
            {
                response.Data = iks;
                response.IsSuccess = true;
                response.Message = "Actualizacion exitosa!!";
                _logger.LogInformation("Consulta exitosa!!");
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Hubo error al actualizar el registro";
                _logger.LogWarning("La respuesta del metodo actualizar es nulo y no se actualizaron los datos");
            }

        }
        catch(Exception ex) 
        {
            response.IsSuccess = false;
            response.Message = $"Ocurrió un error: { ex.Message }";
            _logger.LogError($"Ocurrio un error de servidor y se lanza una excepcion => {ex.Message} ***");
        }
        
        return response;    

    }

    public async Task<Response<bool>> Eliminar(long id)
    {
        var response = new Response<bool>();

        if (id == 0)
        {
            response.IsSuccess = false;
            response.Message = "Debe proporcionar el id del iks a eliminar.";
            _logger.LogWarning("Hubo un error al no proporcionar el id del registro que se va a eliminar");
            return response;
        }


        try
        {
            var iks = await _iksRepositorio.Eliminar(id);

            if (iks)
            {
                response.IsSuccess = true;
                response.Message = "Elimiacion exitosa!!";
                _logger.LogInformation("Elimiacion exitosa!!");
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Hubo error al eliminar el registro";
                _logger.LogWarning("La respuesta del metodo eliminar es falso y no se eliminó el registro");
            }
        }
        catch(Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Ocurrió un error: {ex.Message}";
            _logger.LogError($"Ocurrio un error de servidor y se lanza una excepcion => {ex.Message} ***");
        }
        return response;
    }

    public async Task<Response<bool>> Guardar(IksDto modelo)
    {
       var response = new Response<bool>();

        try
        {

            var validation = _iksDtoValidador.Validate(new IksDto()
            {
                IdCliente = modelo.IdCliente,
                CodigoDeLlave = modelo.CodigoDeLlave,
                UsuarioQueRegistra= modelo.UsuarioQueRegistra,
                IpDeRegistro = modelo.IpDeRegistro

            });

            if (!validation.IsValid)
            {
                response.IsSuccess = false;
                response.Message = "Errores de validación encontrados";
                response.Errors = validation.Errors;
                return response;

            }


            var iks = await _iksRepositorio.Guardar(modelo);

            if (iks is { })
            {
                response.Data = iks;
                response.IsSuccess = true;
                response.Message = "Registro exitoso!!"; 
             _logger.LogInformation("Registro exitoso!!");
        }
            else
            {
                response.IsSuccess = false;
                response.Message = "Hubo error al crear el registro";
                _logger.LogWarning("La respuesta del metodo guardar es nulo y no se guardó el registro");
            }
        }
        catch (Exception ex) 
        {
            response.IsSuccess = false;
            response.Message = $"Ocurrió un error: {ex.Message}";
            _logger.LogError($"Ocurrio un error de servidor y se lanza una excepcion => {ex.Message} ***");

        }

        return response;
    }

    public async Task<Response<IksDto>> ObtenerPorId(long id)
    {
        var response = new Response<IksDto>();

        if (id == 0)
        {
            response.IsSuccess = false;
            response.Message = "Debe proporcionar el id del iks a consultar.";
            _logger.LogWarning("Hubo un error al no proporcionar el id del registro que se va a obtener");
            return response;
        }

        try
        {
            var iks = await _iksRepositorio.ObtenerPorId(id);

            if (iks is { })
            {
                response.Data = iks;
                response.IsSuccess = true;
                response.Message = "Consulta exitosa!!";
                _logger.LogInformation("Consulta exitosa!!");
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "El iks no existe";
                _logger.LogWarning("La respuesta del metodo obtener por id es nulo y no se obtuvo el registro");
            }
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Ocurrió un error: {ex.Message}";
            _logger.LogError($"Ocurrio un error de servidor y se lanza una excepcion => {ex.Message} ***");
        }
        return response;
    }

    public async Task<Response<IEnumerable<IksDto>>> ObtenerTodo()
    {
        var response = new Response<IEnumerable<IksDto>>();

        try
        {
            var iks = await _iksRepositorio.ObtenerTodo();

            if (iks is { })
            {
                response.Data = iks;
                response.IsSuccess = true;
                response.Message = "Consulta exitosa!!";
                _logger.LogInformation("Consulta exitosa!!");
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Hubo error al obtener los registros";
                _logger.LogWarning("La respuesta del metodo obtener todo es nulo y no se obtuvieron los registros");
            }
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Ocurrió un error: {ex.Message}";
            _logger.LogError($"Ocurrio un error de servidor y se lanza una excepcion => {ex.Message} ***");
        }

        return response;
    }

    public async Task<ResponsePagination<IEnumerable<IksDto>>> ObtenerTodoConPaginacion(int numeroPagina, int tamañoPagina)
    {
        var response = new ResponsePagination<IEnumerable<IksDto>>();

        try
        {
            var contador = await _iksRepositorio.Contar();

            var iks = await _iksRepositorio.ObtenerTodoConPaginacion(numeroPagina, tamañoPagina);
          //  response.Data = _mapper.Map<IEnumerable<IksDto>>(iks);

            if (iks != null)
            {
                response.NumeroDePagina = numeroPagina;
                response.TotalPaginas = (int)Math.Ceiling(contador / (double)tamañoPagina);
                response.CantidadTotal = contador;
                response.IsSuccess = true;
                response.Message = "Consulta paginada exitosa!!!";
                _logger.LogInformation("Consulta paginada exitosa!!");

            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Hubo error al consultar los registros";
                _logger.LogWarning("La respuesta del metodo obtener con paginacion es nulo y no se obtuvieron los registros");

            }
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Ocurrió un error: {ex.Message}";
            _logger.LogError($"Ocurrio un error de servidor y se lanza una excepcion => {ex.Message} ***");
        }

        return response;
    } 
}
