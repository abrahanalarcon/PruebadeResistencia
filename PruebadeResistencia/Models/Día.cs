using System;
using System.Collections.Generic;

namespace PruebadeResistencia.Models;

public partial class Día
{
    public int Id { get; set; }

    public string? TipoDeDia { get; set; }

    public virtual ICollection<Resistencium> Resistencia { get; set; } = new List<Resistencium>();
}
