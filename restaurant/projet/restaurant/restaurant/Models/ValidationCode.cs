using System;
using System.Collections.Generic;

namespace restaurant.Models;

public partial class ValidationCode
{
    public int Code { get; set; }

    public DateTime Expiration { get; set; }

    public string? UserMail { get; set; } = null!;
}
