using System;
using System.Linq;
using YetAnotherNoteTaker.Client.Common.Dtos;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.State
{
    public static class UserState
    {
        public static bool IsAuthenticated()
        {
            return UserStateImpl.Instance.CurrentUser != null;
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

        public static string UserEmail => UserStateImpl.Instance.CurrentUser?.Email ?? string.Empty;
        public static string Token => UserStateImpl.Instance.CurrentUser?.AccessToken ?? string.Empty;

        public static void SetUser(LoggedInUserDto user)
        {
            UserStateImpl.Instance.CurrentUser = user;
        }
    }
}
