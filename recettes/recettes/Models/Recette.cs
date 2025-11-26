using System;
using System.Collections.Generic;

namespace recettes.Models;

public partial class Recette
{
    public int IdRecette { get; set; }

    public string Nom { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string TempsPreparation { get; set; } = null!;

    public string TempsCuisson { get; set; } = null!;

    public int NbrePortions { get; set; }

    public int Calorie { get; set; }

    public string Proteine { get; set; } = null!;

    public string MatiereGrasse { get; set; } = null!;

    public string Glucide { get; set; } = null!;

    public string Fibre { get; set; } = null!;

    public string Fer { get; set; } = null!;

    public string Calcium { get; set; } = null!;

    public string Sodium { get; set; } = null!;

    // Ajout des propriétés de navigation pour Ingredients et Etapes
    public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    public virtual ICollection<Etape> Etapes { get; set; } = new List<Etape>();
}