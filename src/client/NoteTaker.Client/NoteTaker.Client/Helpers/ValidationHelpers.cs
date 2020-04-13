using System.Text.RegularExpressions;

namespace NoteTaker.Client.Helpers
{
    public static class ValidationHelpers
    {
        private static Regex s_emailRegex = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            return s_emailRegex.IsMatch(email);
        }
    }
}
