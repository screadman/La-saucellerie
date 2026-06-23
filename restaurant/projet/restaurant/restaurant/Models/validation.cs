using System.Security.Cryptography;

namespace restaurant.Models
{
    public class Validation
    {
        public static bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            // Séparation des composants : {salt}.{hash}.{iterations}
            string[] parts = storedPassword.Split('.');
            if (parts.Length != 3)
                throw new FormatException("Le mot de passe enregistré n'est pas dans le bon format.");

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] storedHash = Convert.FromBase64String(parts[1]);
            int iterations = int.Parse(parts[2]);

            // Hachage du mot de passe entré avec les mêmes paramètres
            using (var algorithm = new Rfc2898DeriveBytes(enteredPassword, salt, iterations, HashAlgorithmName.SHA512))
            {
                byte[] hash = algorithm.GetBytes(storedHash.Length);

                // Comparaison des deux hachages
                return CryptographicOperations.FixedTimeEquals(hash, storedHash);
            }
        }
    }
}
