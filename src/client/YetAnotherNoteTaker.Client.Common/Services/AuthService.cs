using System;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Data;
using YetAnotherNoteTaker.Client.Common.Dtos;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
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

            return new LoggedInUserDto
            {
                AccessToken = accessToken,
                Email = email
            };
        }
    }
}
