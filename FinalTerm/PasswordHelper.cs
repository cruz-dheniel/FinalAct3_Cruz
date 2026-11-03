
using System;
using System.Security.Cryptography;

namespace FinalTerm
{
    public static class PasswordHelper
    {
        // Converts a password into a secure hash
        public static string HashPassword(string password)
        {
            int iterations = 150000;
            byte[] salt = new byte[16];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(
                password, salt, iterations))
            {
                byte[] hash = pbkdf2.GetBytes(32);

                return "PBKDF2$" + iterations + "$" +
                    Convert.ToBase64String(salt) + "$" +
                    Convert.ToBase64String(hash);
            }
        }

        // Checks if the entered password is correct
        public static bool VerifyPassword(
            string password, string storedHash)
        {
            try
            {
                string[] parts = storedHash.Split('$');

                if (parts.Length != 4 ||
                    parts[0] != "PBKDF2")
                    return false;

                int iterations = int.Parse(parts[1]);

                if (iterations < 10000 || iterations > 1000000)
                    return false;

                byte[] salt =
                    Convert.FromBase64String(parts[2]);

                byte[] originalHash =
                    Convert.FromBase64String(parts[3]);

                using (var pbkdf2 =
                    new Rfc2898DeriveBytes(
                        password, salt, iterations))
                {
                    byte[] newHash = pbkdf2.GetBytes(32);

                    if (newHash.Length != originalHash.Length)
                        return false;

                    int difference = 0;

                    for (int i = 0; i < newHash.Length; i++)
                    {
                        difference |= newHash[i] ^ originalHash[i];
                    }

                    return difference == 0;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
