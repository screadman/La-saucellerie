using System.Security.Cryptography;

namespace restaurant.Models
{
    public class HashPassword
    {
        public static string HashPass(string password)
        {
            // Paramètres de hachage
            int saltSize = 16; // Taille du sel en octets
            int keySize = 32;  // Taille de la clé dérivée (32 octets = 256 bits)
            int iterations = 10000; // Nombre d'itérations

            // Génération d'un sel aléatoire
            byte[] salt = new byte[saltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Hachage avec PBKDF2
            using (var algorithm = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA512))
            {
                byte[] hash = algorithm.GetBytes(keySize);

                // Combinaison des éléments pour stockage : {salt}.{hash}.{iterations}
                return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}.{iterations}";
            }
        }
    }

}

