using System;
using System.Collections.Generic;

namespace recettes.Models;

public partial class Ingredient
{
    public int IdIngredient { get; set; }

    public int IdRecette { get; set; }

    public string Quantite { get; set; } = null!;

    public string Ingredient1 { get; set; } = null!;
}
