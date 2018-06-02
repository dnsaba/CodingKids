using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace CodingKids.Services.Cryptography
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

        public string Encrypt(string plaintext, string purpose = "")
        {
            byte[] bytes = Encoding.UTF8.GetBytes(plaintext);
            byte[] cipherBytes = MachineKey.Protect(bytes, purpose);
            string encodedCipherBytes = Convert.ToBase64String(cipherBytes);
            return encodedCipherBytes;
        }

        public bool TryDecrypt(string encodedCiphertext, out string plaintext)
        {
            return TryDecrypt(encodedCiphertext, "", out plaintext);
        }

        public bool TryDecrypt(string encodedCiphertext, string purpose, out string plaintext)
        {
            byte[] cipherBytes;

            try
            {
                cipherBytes = Convert.FromBase64String(encodedCiphertext);
            }
            catch (FormatException)
            {
                plaintext = null;
                return false;
            }

            try
            {
                byte[] bytes = MachineKey.Unprotect(cipherBytes, purpose);
                plaintext = Encoding.UTF8.GetString(bytes);

                return true;
            }
            catch (CryptographicException)
            {
                plaintext = null;
                return false;
            }
        }

        public string Hash(string message, string messHashKey)
        {
            string key = messHashKey;

            byte[] keyBytes = Convert.FromBase64String(key);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] hash;
            using (HMACSHA256 hasher = new HMACSHA256(keyBytes))
            {
                hash = hasher.ComputeHash(messageBytes);
            }
            return Convert.ToBase64String(hash);
        }

        public string Hash(string original, string salt, int iterations = 1)
        {
            const int hashByteSize = 20;

            byte[] saltBytes = Convert.FromBase64String(salt);

            byte[] bytes;
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(original, saltBytes))
            {
                pbkdf2.IterationCount = iterations;
                bytes = pbkdf2.GetBytes(hashByteSize);
            }

            return Convert.ToBase64String(bytes);
        }

        public static bool SlowEquals(string x, string y)
        {
            byte[] a = Convert.FromBase64String(x);
            byte[] b = Convert.FromBase64String(y);

            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }
    }
}
