using Iks.WebApi.Aplicacion.Interfaces;
using Iks.WebApi.Dominio.DTOs;
using Iks.WebApi.Dominio.Interfaces;
using Iks.WebApi.Transversal.Interfaces;
using Iks.WebApi.Transversal.Modelos;

namespace Iks.WebApi.Aplicacion.Servicios
{
    public class IksServicio : IIksServicio
    {
        private readonly IIksRepositorio _iksRepositorio;
        private readonly IAppLogger<IksServicio> _logger;

        public IksServicio(IIksRepositorio iksRepositorio, IAppLogger<IksServicio> logger)
        {
            _iksRepositorio = iksRepositorio;
            _logger = logger;
        }

        public async Task<Response<bool>> Actualizar(long id, IksDto modelo)
        {
            var response = new Response<bool>();

            if (id == 0)
            {
                response.IsSuccess = false;
                response.Message = "Debe proporcionar el id del iks a actualizar.";
                return response;
            }
            try
            {
                var iksExitente = Obtener(id);

                if (iksExitente is null)
                {
                    response.IsSuccess = false;
                    response.Message = "El iks a actualizar no existe";
                    return response;

                }

                var iks = await _iksRepositorio.Actualizar(modelo);

                if (iks is { }) // no es nulo
                {
                    response.Data = iks;
                    response.IsSuccess = true;
                    response.Message = "Actualizacion exitosa!!";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Hubo error al actualizar el registro";
                }

            }
            catch(Exception ex) 
            {
                response.IsSuccess = false;
                response.Message = $"Ocurrió un error: { ex }";
            }
            
            return response;    

        }

        public Task<Response<int>> Contar()
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> Eliminar(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<bool>> Guardar(IksDto modelo)
        {
           var response = new Response<bool>();

            try
            {

                //var validation = _IksDtoValidador.Validate(new IksDto()
                //{
                //    IdIndicativo = ClienteDto.IdIndicativo,
                //    IdCiudad = ClienteDto.IdCiudad,
                //    PrimerNombre = ClienteDto.PrimerNombre,
                //    SegundoNombre = ClienteDto.SegundoNombre,
                //    PrimerApellido = ClienteDto.PrimerApellido,
                //    SegundoApellido = ClienteDto.SegundoApellido,
                //    Telefono = ClienteDto.Telefono,
                //    UsuarioQueRegistraPersona = ClienteDto.UsuarioQueRegistraPersona,
                //    UsuarioQueRegistraCliente = ClienteDto.UsuarioQueRegistraCliente

                //});

                //if (!validation.IsValid)
                //{
                //    response.IsSuccess = false;
                //    response.Message = "Errores de validación encontrados";
                //    response.Errors = validation.Errors;
                //    return response;

                //}


                var iks = await _iksRepositorio.Guardar(modelo);

                if (iks is { })
                {
                    response.Data = iks;
                    response.IsSuccess = true;
                    response.Message = "Registro exitoso!!";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Hubo error al crear el registro";
                }
            }
            catch (Exception ex) 
            {
                response.IsSuccess = false;
                response.Message = $"Ocurrió un error: {ex.Message}";

            }

            return response;
        }

        public async Task<Response<IksDto>> Obtener(long id)
        {
            var response = new Response<IksDto>();

            if (id == 0)
            {
                response.IsSuccess = false;
                response.Message = "Debe proporcionar el id del iks a consultar.";
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
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "El iks no existe";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Ocurrió un error: {ex.Message}";
            }
            return response;
        }

        public Task<Response<IEnumerable<IksDto>>> ObtenerTodo()
        {
            throw new NotImplementedException();
        }

        public Task<Response<IEnumerable<IksDto>>> ObtenerTodoConPaginacion(int numeroPagina, int tamañoPagina)
        {
            throw new NotImplementedException();
        }
    }
}
