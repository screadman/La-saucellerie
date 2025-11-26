using System;
using System.Collections.Generic;

namespace recettes.Models;

public partial class Etape
{
    public int IdEtape { get; set; }

    public int IdRecette { get; set; }

    public int NoEtape { get; set; }

    public string Description { get; set; } = null!;
}
