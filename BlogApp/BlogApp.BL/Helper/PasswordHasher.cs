using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.BL.Helper
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltByteSize = 24;
        private const int HashByteSize = 24;
        private const int HasingIterationsCount = 10101;
        public string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, SaltByteSize, HasingIterationsCount))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(HashByteSize);
            }
            byte[] dst = new byte[(SaltByteSize + HashByteSize) + 1];
            Buffer.BlockCopy(salt, 0, dst, 1, SaltByteSize);
            Buffer.BlockCopy(buffer2, 0, dst, SaltByteSize + 1, HashByteSize);
            return Convert.ToBase64String(dst);
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            var hashBytes = Convert.FromBase64String(hashedPassword);

            // Extract the salt
            var salt = new byte[SaltByteSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltByteSize);

            // Hash the provided password with the extracted salt
            var key = Rfc2898DeriveBytes.Pbkdf2(
                providedPassword,
                salt,
                HasingIterationsCount,
                HashAlgorithmName.SHA256,
                HashByteSize
            );

            // Extract the stored key
            var storedKey = new byte[HashByteSize];
            Array.Copy(hashBytes, SaltByteSize, storedKey, 0, HashByteSize);

            // Compare the stored key with the newly hashed key
            var isMatch = CryptographicOperations.FixedTimeEquals(key, storedKey);

            return isMatch ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }
    }
}
