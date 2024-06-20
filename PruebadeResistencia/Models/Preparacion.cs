using System;
using System.Collections.Generic;

namespace PruebadeResistencia.Models;

public partial class Preparacion
{
    public int Id { get; set; }

    public DateOnly? FechaDePreparación { get; set; }

    public int? MolinoId { get; set; }

    public int? CementoId { get; set; }

    public virtual Cemento? Cemento { get; set; }

    public virtual Molino? Molino { get; set; }

    public virtual ICollection<Resistencium> Resistencia { get; set; } = new List<Resistencium>();
}
