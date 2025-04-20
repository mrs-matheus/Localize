using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace Localize.Company.Domain.Cryptographs
{
    public class CryptPassword
    {
        public static string Hash(string password)
        {
            byte[] salt = new byte[16];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var saltBase = Convert.ToBase64String(salt);
            var paswordBase = HashPassword(salt, password).Replace("==", "");

            return paswordBase + ":" + Convert.ToBase64String(salt).Replace("==", "");

        }

        public static bool Verify(string password, string storedPassword)
        {
            string[] parts = storedPassword.Split(':');

            if (parts.Count() != 2)
                return false;

            string storedHash = parts[0] + "==";
            string storedSalt = parts[1] + "==";

            byte[] salt = Convert.FromBase64String(storedSalt);

            return storedHash == HashPassword(salt, password);
        }

        private static string HashPassword(byte[] salt, string password)
        {
            using (var hasher = new Argon2id(Encoding.UTF8.GetBytes(password)))
            {
                hasher.Salt = salt;
                hasher.DegreeOfParallelism = 2;
                hasher.MemorySize = 4096;
                hasher.Iterations = 2;

                byte[] hash = hasher.GetBytes(16);

                return Convert.ToBase64String(hash);
            }
        }
    }
}
