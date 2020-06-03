using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Data;
using YetAnotherNoteTaker.Client.Common.Http;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Blazor.Data
{
    public class BlazorAuthRepository : IAuthRepository
    {
        private readonly IRestClient _restClient;
        private readonly IUrlBuilder _urlBuilder;

        public BlazorAuthRepository(IRestClient restClient, IUrlBuilder urlBuilder)
        {
            _restClient = restClient;
            _urlBuilder = urlBuilder;
        }

        public Task<UserDto> CreateUser(NewUserDto newUserDto)
        {
            var postUrl = _urlBuilder.Users.Post;
            return _restClient.Post<UserDto>(postUrl, newUserDto);
        }

        public Task<string> GetAuthToken(string email, string password)
        {
            var authUrl = _urlBuilder.Users.Auth;
            return _restClient.Authenticate(authUrl, email, password);
        }
    }
}
