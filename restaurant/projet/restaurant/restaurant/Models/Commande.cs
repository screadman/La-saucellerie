using System;
using System.Collections.Generic;

namespace restaurant.Models;

public partial class Commande
{
    public int IdReservation { get; set; }

    public int IdRepas { get; set; }

    public int? Quantite { get; set; }

    public virtual Repa IdRepasNavigation { get; set; } = null!;

    public virtual Reservation IdReservationNavigation { get; set; } = null!;
}
