using System;
using System.Collections.Generic;

namespace utilisateurs.Models;

public partial class Utilisateur
{
    public int IdUtilisateur { get; set; }

    public int IdTypeUtilisateur { get; set; }

    public string? Courriel { get; set; } 

    public string? MotPasse { get; set; } 

    public virtual TypeUtilisateur IdTypeUtilisateurNavigation { get; set; } = null!;
}
public class AuthentificationRequest
{
    public string? mail { get; set; }
    public string? MotPasse { get; set; }
}
