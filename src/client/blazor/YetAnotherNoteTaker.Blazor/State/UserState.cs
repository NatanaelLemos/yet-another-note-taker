using System;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using YetAnotherNoteTaker.Client.Common.Dtos;
using YetAnotherNoteTaker.Client.Common.State;

namespace YetAnotherNoteTaker.Blazor.State
{
    public class UserState : IUserState
    {
        private const string TokenKey = "a_tk";
        private const string EmailKey = "a_ue";

        private readonly ILocalStorageService _localStorage;

        public UserState(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<bool> IsAuthenticated()
        {
            var token = await Token;
            return !string.IsNullOrEmpty(token);
        }

        public Task<bool> IsAuthenticated(Type pageType)
        {
            return IsAuthenticated();
        }

        public Task<string> UserEmail
        {
            get
            {
                try
                {
                    return _localStorage.GetItemAsync<string>(EmailKey);
                }
                catch
                {
                    return Task.FromResult(string.Empty);
                }
            }
        }

        public Task<string> Token
        {
            get
            {
                try
                {
                    return _localStorage.GetItemAsync<string>(TokenKey);
                }
                catch
                {
                    return Task.FromResult(string.Empty);
                }
            }
        }

        public async Task SetUser(LoggedInUserDto user)
        {
            await _localStorage.SetItemAsync(EmailKey, user.Email);
            await _localStorage.SetItemAsync(TokenKey, user.AccessToken);
        }
    }
}
