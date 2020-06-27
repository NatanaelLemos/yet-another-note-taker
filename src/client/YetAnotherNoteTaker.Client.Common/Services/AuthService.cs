using System;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Data;
using YetAnotherNoteTaker.Client.Common.Dtos;
using YetAnotherNoteTaker.Client.Common.State;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IUserState _userState;

        public AuthService(IAuthRepository authRepository, IUserState userState)
        {
            _authRepository = authRepository;
            _userState = userState;
        }

        public Task<UserDto> CreateUser(NewUserDto newUserDto)
        {
            return _authRepository.CreateUser(newUserDto);
        }

        public async Task<LoggedInUserDto> Login(string email, string password)
        {
            var accessToken = await _authRepository.GetAuthToken(email, password);

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new NullReferenceException("Invalid email or password");
            }

            var loggedUser = new LoggedInUserDto
            {
                AccessToken = accessToken,
                Email = email
            };

            await _userState.SetUser(loggedUser);
            return loggedUser;
        }
    }
}
