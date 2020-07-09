using System;
using System.Linq;
using Microsoft.AspNetCore.WebUtilities;

namespace YetAnotherNoteTaker.Blazor.Helpers
{
    public static class NotebookHelpers
    {
        public static string GetKeyFromUri(Uri uri)
        {
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("notebookKey", out var param))
            {
                return param.First();
            }

            return null;
        }
    }
}
