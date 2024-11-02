using System;
using System.Collections.Generic;

namespace Iks.WebApi.Dominio.Persistencia.Modelos;

public partial class Cliente
{
    public long IdCliente { get; set; }

    public long IdPersona { get; set; }

    public bool? Estado { get; set; }

    public bool? EstadoEliminado { get; set; }

    public string UsuarioQueRegistra { get; set; } = null!;

    public string? UsuarioQueActualiza { get; set; }

    public DateOnly FechaDeRegistro { get; set; }

    public TimeOnly HoraDeRegistro { get; set; }

    public string IpDeRegistro { get; set; } = null!;

    public DateOnly? FechaDeActualizado { get; set; }

    public TimeOnly? HoraDeActualizado { get; set; }

    public string? IpDeActualizado { get; set; }

    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual ICollection<Ik> Iks { get; set; } = new List<Ik>();
}
