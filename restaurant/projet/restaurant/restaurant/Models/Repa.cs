using System;
using System.Collections.Generic;

namespace restaurant.Models;

public partial class Repa
{
    public int IdRepas { get; set; }

    public string? Nom { get; set; }

    public string? Description { get; set; }

    public string? Prix { get; set; }

    public int? Calories { get; set; }

    public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();
}
