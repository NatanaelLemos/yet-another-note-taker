using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Data;
using YetAnotherNoteTaker.Client.Common.Http;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Data
{
    public class XamarinAuthRepository : IAuthRepository
    {
        private readonly IRestClient _restClient;
        private readonly IUrlBuilder _urlBuilder;

        public XamarinAuthRepository(IRestClient restClient, IUrlBuilder urlBuilder)
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
