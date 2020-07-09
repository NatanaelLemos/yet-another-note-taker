using Microsoft.AspNetCore.Components.Web;

namespace YetAnotherNoteTaker.Blazor.Extensions
{
    public static class HtmlInputExtensions
    {
        public static bool IsSubmit(this KeyboardEventArgs e)
        {
            var key = e?.Key ?? string.Empty;
            return key.ToLowerInvariant() == "enter";
        }
    }
}
