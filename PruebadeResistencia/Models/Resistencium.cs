using System;
using System.Collections.Generic;

namespace PruebadeResistencia.Models;

public partial class Resistencium
{
    public int Id { get; set; }

    public int? Cubo1 { get; set; }

    public int? Cubo2 { get; set; }

    public int? Cubo3 { get; set; }

    public int? Prom { get; set; }

    public int? PreparacionId { get; set; }

    public int? DiaId { get; set; }

    public virtual Día? Dia { get; set; }

    public virtual Preparacion? Preparacion { get; set; }
}
