using System;
using System.Linq;

namespace NoteTaker.Client.State
{
    public static class UserState
    {
        public static bool UserIsAuthenticated { get; set; } = false;

        public static bool IsAuthenticated()
        {
            return UserIsAuthenticated;
        }

        public static bool IsAuthenticated(Type pageType)
        {
            var anonymousAttr = pageType.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(AllowAnonymousAttribute));
            if (anonymousAttr != null || IsAuthenticated())
            {
                return true;
            }

            return false;
        }
    }
}
