using System;
using System.Security.Cryptography;
using System.Text;

namespace StudyScraper.Services
{
    public class CryptographyService
    {
        public string GenerateRandomString(int length)
        {
            byte[] bytes = new byte[(int)Math.Floor(length * .75)];
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }
            return Convert.ToBase64String(bytes);
        }

        public string Hash(string password, string salt)
        {
            string key = salt;
            //convert the salt to bas64 becasue for sha256 the recommended key is 64 bytes
            byte[] keyBytes = Convert.FromBase64String(key);
            byte[] messageBytes = Encoding.UTF8.GetBytes(password);
            byte[] hash;
            using (HMACSHA256 hasher = new HMACSHA256(keyBytes))
            {
                hash = hasher.ComputeHash(messageBytes);
            }

            //convert the hashed bytes to a base 64 string
            return Convert.ToBase64String(hash);
        }
    }
}
