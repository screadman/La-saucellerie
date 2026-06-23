using System;
using System.Collections.Generic;

namespace restaurant.Models;

public partial class Client
{
    public int IdClient { get; set; }

    public string Username { get; set; } = null!;

    public string? EMail { get; set; }

    public string Mdp { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
