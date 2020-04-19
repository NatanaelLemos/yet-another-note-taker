using System.Security.Cryptography;
using System.Text;

namespace NoteTaker.Domain.Helpers
{
    public static class EncryptionHelpers
    {
        public static string Hash(string plainText)
        {
            var data = Encoding.UTF8.GetBytes(plainText);
            using (var shaM = new SHA512Managed())
            {
                var hash = shaM.ComputeHash(data);
                return Encoding.UTF8.GetString(hash);
            }
        }
    }
}
