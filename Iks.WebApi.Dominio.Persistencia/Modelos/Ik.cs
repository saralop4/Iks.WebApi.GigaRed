namespace Iks.WebApi.Dominio.Persistencia.Modelos;

public partial class Ik
{
    public long IdIks { get; set; }

    public long IdCliente { get; set; }

    public string CodigoDeLlave { get; set; } = null!;

    public bool? EstadoEliminado { get; set; }

    public string UsuarioQueRegistra { get; set; } = null!;

    public string? UsuarioQueActualiza { get; set; }

    public DateOnly FechaDeRegistro { get; set; }

    public TimeOnly HoraDeRegistro { get; set; }

    public string IpDeRegistro { get; set; } = null!;

    public DateOnly? FechaDeActualizado { get; set; }

    public TimeOnly? HoraDeActualizado { get; set; }

    public string? IpDeActualizado { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;
}
