namespace restaurant.Models
{
    public class intermediaiaire
    {
        public int Code { get; set; }

        public DateTime Expiration { get; set; }

        public string? UserMail { get; set; } = null!;
        public string? Username { get; set; } = null!;  
        public string? Mdp { get; set; } = null!;
    }

}

