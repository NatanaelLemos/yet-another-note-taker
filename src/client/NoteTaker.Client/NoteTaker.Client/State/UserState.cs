using System;
using System.Linq;
using NoteTaker.Domain.Dtos;

namespace NoteTaker.Client.State
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

        public static UserDto CurrentUser
        {
            set => UserStateImpl.Instance.CurrentUser = value;
        }

        public static Guid UserId => UserStateImpl.Instance.CurrentUser?.Id ?? Guid.Empty;
    }
}
