using System.Security.Cryptography;
using System.Text;

namespace YetAnotherNoteTaker.Common.Helpers
{
    public static class EncryptionHelpers
    {
        public static string Hash(string raw)
        {
            using var shaHash = SHA512.Create();
            var bytes = shaHash.ComputeHash(Encoding.UTF8.GetBytes(raw));

            var builder = new StringBuilder();
            foreach (var t in bytes)
            {
                builder.Append(t.ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
