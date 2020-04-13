using System;
using System.Threading.Tasks;
using NoteTaker.Domain.Data;
using NoteTaker.Domain.Dtos;
using NoteTaker.Domain.Entities;

namespace NoteTaker.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<UserDto> GetByEmail(string email)
        {
            var user = await _authRepository.GetByEmail(email);

            if (user == null)
            {
                return null;
            }

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email
            };
        }

        public async Task CreateUser(UserDto userDto)
        {
            var dbUser = await GetByEmail(userDto.Email);
            if (dbUser != null)
            {
                throw new InvalidOperationException("Email already registered");
            }

            var user = new User
            {
                Email = userDto.Email,
                Password = userDto.Password
            };

            await _authRepository.Create(user);
            await _authRepository.Save();
        }
    }
}
