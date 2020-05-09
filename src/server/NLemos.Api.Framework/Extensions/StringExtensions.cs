using System.Globalization;
using System.Text;

namespace NLemos.Api.Framework.Extensions
{
    public static class StringExtensions
    {
        public static string GenerateKey(this string text)
        {
            text = FormatString(text);
            text = RemoveDiacritics(text);

            var textBuilder = new StringBuilder();

            foreach (var c in text)
            {
                if (char.IsLetterOrDigit(c))
                {
                    textBuilder.Append(c);
                }
            }

            return textBuilder.ToString().ToLowerInvariant();
        }

        private static string FormatString(string text)
        {
            if (text.StartsWith("\""))
            {
                text = text.Substring(1);
            }

            if (text.EndsWith("\""))
            {
                text = text.Substring(0, text.Length - 1);
            }

            return text.Trim();
        }

        public static string RemoveDiacritics(this string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
