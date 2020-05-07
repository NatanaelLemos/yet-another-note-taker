using System;
using System.Linq;
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

        public static Guid UserId => UserStateImpl.Instance.CurrentUser?.Id ?? Guid.Empty;

        public static void SetUser(UserDto user)
        {
            UserStateImpl.Instance.CurrentUser = user;
        }
    }
}
