using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace YetAnotherNoteTaker.Web.State
{
    public class UserState : IUserState
    {
        private const string UserEmailKey = "userEmail";
        private const string TokenKey = "token";

        private readonly ILocalStorageService _localStorage;

        public UserState(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<bool> IsAuthenticated()
        {
            var token = await GetToken();
            return !string.IsNullOrWhiteSpace(token);
        }

        public Task<string> GetUserEmail()
        {
            return _localStorage.GetItemAsync<string>(UserEmailKey);
        }

        public Task SetUserEmail(string email)
        {
            return _localStorage.SetItemAsync(UserEmailKey, email);
        }

        public Task<string> GetToken()
        {
            return _localStorage.GetItemAsync<string>(TokenKey);
        }

        public Task SetToken(string token)
        {
            return _localStorage.SetItemAsync(TokenKey, token);
        }
    }
}
