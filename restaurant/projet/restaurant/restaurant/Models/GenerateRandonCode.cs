using System.Security.Cryptography;
public class GenerateRandomCode
{
    public static string Generate(int length)
    {
        const string validChars = "0123456789";
        using (var rng = RandomNumberGenerator.Create())
        {
            var byteArray = new byte[length];
            rng.GetBytes(byteArray);
            var chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[byteArray[i] % validChars.Length];
            }
            return new string(chars);
        }
    }
}

