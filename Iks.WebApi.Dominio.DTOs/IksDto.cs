namespace Iks.WebApi.Dominio.DTOs;

public class IksDto
{
    public long? IdIks { get; set; }
    public long? IdCliente { get; set; }
    public string? CodigoDeLlave { get; set; }
    public bool? EstadoEliminado { get; set; }
    public string? UsuarioQueRegistra { get; set; }
    public string? UsuarioQueActualiza { get; set; }
    public string? IpDeRegistro { get; set; }

  //  [JsonPropertyName("FechaDeActualizado")]
    public DateTime? FechaDeActualizado { get; set; }

 //   [JsonPropertyName("HoraDeActualizado")]
    public TimeSpan? HoraDeActualizado { get; set; }

  //  [JsonPropertyName("IpDeActualizado")]
    public string? IpDeActualizado { get; set; }

}
