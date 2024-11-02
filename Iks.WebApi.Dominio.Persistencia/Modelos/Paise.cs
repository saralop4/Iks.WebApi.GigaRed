using System;
using System.Collections.Generic;

namespace Iks.WebApi.Dominio.Persistencia.Modelos;

public partial class Paise
{
    public long IdPais { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Ciudade> Ciudades { get; set; } = new List<Ciudade>();
}
