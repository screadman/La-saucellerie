using System;
using System.Collections.Generic;

namespace restaurant.Models;

public partial class Reservation
{
    public int IdReservation { get; set; }

    public int IdClient { get; set; }

    public DateOnly? DateReservation { get; set; }

    public TimeOnly? Heure { get; set; }

    public int? NbrePlace { get; set; }

    public virtual Commande? Commande { get; set; }

    public virtual Client IdClientNavigation { get; set; } = null!;
}
