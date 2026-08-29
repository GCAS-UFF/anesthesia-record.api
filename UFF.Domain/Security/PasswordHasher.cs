using System.Security.Cryptography;

namespace UFF.FichaAnestesica.Domain.Security
{
    /// <summary>
    /// PBKDF2 password hashing. Verify() also accepts legacy plain-text values
    /// (anything not in the "v1.iterations.salt.hash" format) so existing rows
    /// keep working until the password is changed, at which point it is stored hashed.
    /// </summary>
    public static class PasswordHasher
    {
        private const string FormatTag = "v1";
        private const int SaltSizeBytes = 16;
        private const int HashSizeBytes = 32;
        private const int Iterations = 100_000;

        public static string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSizeBytes);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, HashSizeBytes);

            return string.Join('.', FormatTag, Iterations, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public static bool Verify(string password, string? stored)
        {
            if (string.IsNullOrEmpty(stored))
                return false;

            var parts = stored.Split('.');

            if (parts.Length != 4 || parts[0] != FormatTag || !int.TryParse(parts[1], out var iterations))
                return stored == password; // legacy plain-text row

            var salt = Convert.FromBase64String(parts[2]);
            var expectedHash = Convert.FromBase64String(parts[3]);
            var actualHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, expectedHash.Length);

            return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
        }

        public static bool IsHashed(string? stored)
        {
            if (string.IsNullOrEmpty(stored))
                return false;

            var parts = stored.Split('.');
            return parts.Length == 4 && parts[0] == FormatTag;
        }
    }
}
