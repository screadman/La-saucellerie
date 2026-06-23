namespace restaurant.Models
{
    public class AuthentificationRequest
    { 
        public string Username { get; set; } = null!;

        public string? EMail { get; set; }

        public string Mdp { get; set; } = null!;
    }
}
