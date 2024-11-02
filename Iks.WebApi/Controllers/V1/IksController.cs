using Iks.WebApi.Aplicacion.Interfaces;
using Iks.WebApi.Dominio.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Iks.WebApi.Controllers.V1;

[Authorize]
[Route("Api/V{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]

public class IksController : ControllerBase
{
    private readonly IIksServicio _iksServicio;
    public IksController(IIksServicio iksServicio)
    {
        _iksServicio = iksServicio;
    }

    // GET: Api/<IksController>
    [HttpGet("ObtenerTodo")]
    public async Task<IActionResult> ObtenerTodo()
    {
        var Response = await _iksServicio.ObtenerTodo();

        if (Response.IsSuccess)
        {
            return Ok(Response);
        }
        return BadRequest(Response);
    }

    // GET api/<IksController>/5
    [HttpGet("ObtenerPorId/{Id}")]
    public async Task<IActionResult> ObtenerPorId(long Id)
    {
        var Response = await _iksServicio.ObtenerPorId(Id);

        if (Response.IsSuccess)
        {
            return Ok(Response);
        }
        return BadRequest(Response);
    }

    // POST api/<IksController>
    [HttpPost("Guardar")]
    public async Task<IActionResult> Guardar([FromBody] IksDto Value)
    {
        var ipDeRegistro = HttpContext.Connection.RemoteIpAddress?.ToString();

        if (ipDeRegistro != null)
        {
            Value.IpDeRegistro = ipDeRegistro.ToString();
        }

        var Response = await _iksServicio.Guardar(Value);

        if (Response.IsSuccess)
        {
            return Ok(Response);
        }
        return BadRequest(Response);

    }

    // PUT api/<IksController>/5
    [HttpPut("Actualizar/{Id}")]
    public async Task<IActionResult> Actualizar(long Id, [FromBody] IksDto Value)
    {
        var ipDeActualizado = HttpContext.Connection.RemoteIpAddress?.ToString();

        if (ipDeActualizado != null && Value.IpDeActualizado != null)
        {
            Value.IpDeActualizado = ipDeActualizado.ToString();
        }


        var Response = await _iksServicio.Actualizar(Id, Value);

        if (Response.IsSuccess)
        {
            return Ok(Response);
        }
        return BadRequest(Response);
    }

    // DELETE api/<IksController>/5
    [HttpDelete("Eliminar/{Id}")]
    public async Task<IActionResult> Eliminar(long Id)
    {
        var Response = await _iksServicio.Eliminar(Id);

        if (Response.IsSuccess)
        {
            return Ok(Response);
        }
        return BadRequest(Response);

    }

    [HttpGet("ObtenerTodoConPaginacion/{NumeroPagina}/{TamañoPagina}")]
    public async Task<IActionResult> ObtenerTodoConPaginacion(int NumeroPagina, int TamañoPagina)
    {
        var Response = await _iksServicio.ObtenerTodoConPaginacion(NumeroPagina, TamañoPagina);

        if (Response.IsSuccess)
        {
            return Ok(Response);
        }
        return BadRequest(Response);

    }
}

