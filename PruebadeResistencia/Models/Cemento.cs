using System;
using System.Collections.Generic;

namespace PruebadeResistencia.Models;

public partial class Cemento
{
    public int Id { get; set; }

    public string? Código { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Preparacion> Preparacions { get; set; } = new List<Preparacion>();
}
