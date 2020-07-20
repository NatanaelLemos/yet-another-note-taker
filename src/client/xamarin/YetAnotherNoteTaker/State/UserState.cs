using System;
using System.Linq;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Dtos;
using YetAnotherNoteTaker.Client.Common.Security;
using YetAnotherNoteTaker.Client.Common.State;

namespace YetAnotherNoteTaker.State
{
    public class UserState : IUserState
    {
        private LoggedInUserDto _currentUser;

        public Task<bool> IsAuthenticated()
        {
            return Task.FromResult(_currentUser != null);
        }

        public async Task<bool> IsAuthenticated(object eventObj)
        {
            var anonymousAttr = eventObj.GetType().CustomAttributes
                .FirstOrDefault(a => a.AttributeType == typeof(AllowAnonymousAttribute));

            if (anonymousAttr != null)
            {
                return true;
            }

            return await IsAuthenticated();
        }

        public Task<string> UserEmail => Task.FromResult(_currentUser?.Email ?? string.Empty);

        public Task<string> Token => Task.FromResult(_currentUser?.AccessToken ?? string.Empty);

        public Task SetUser(LoggedInUserDto user)
        {
            _currentUser = user;
            return Task.CompletedTask;
        }
    }
}
