using System;
using System.Collections.Generic;

namespace utilisateurs.Models
{
    public partial class TypeUtilisateur
    {
        public int IdType { get; set; }
        public string? Type { get; set; }

        // Navigation property to the Utilisateurs table
        public virtual ICollection<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();
    }
}