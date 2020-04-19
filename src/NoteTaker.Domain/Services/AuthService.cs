using System;
using System.Threading.Tasks;
using NoteTaker.Domain.Data;
using NoteTaker.Domain.Dtos;
using NoteTaker.Domain.Entities;
using NoteTaker.Domain.Helpers;

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

        public async Task<UserDto> GetByEmailAndPassword(string email, string password)
        {
            var existingUser = await _authRepository.GetByEmail(email);

            if (existingUser == null || !EncryptionHelpers.Hash(password).Equals(existingUser.Password))
            {
                return null;
            }

            return new UserDto
            {
                Id = existingUser.Id,
                Email = existingUser.Email
            };
        }

        public async Task CreateUser(NewUserDto userDto)
        {
            var dbUser = await GetByEmail(userDto.Email);
            if (dbUser != null)
            {
                throw new InvalidOperationException("Email already registered");
            }

            var user = new User
            {
                Email = userDto.Email,
                Password = EncryptionHelpers.Hash(userDto.Password)
            };

            await _authRepository.Create(user);
            await _authRepository.Save();
        }
    }
}
