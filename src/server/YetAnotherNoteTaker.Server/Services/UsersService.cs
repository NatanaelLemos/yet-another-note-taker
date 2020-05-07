using System.Threading.Tasks;
using NLemos.Api.Framework.Exceptions;
using YetAnotherNoteTaker.Common.Dtos;
using YetAnotherNoteTaker.Common.Helpers;
using YetAnotherNoteTaker.Server.Data;
using YetAnotherNoteTaker.Server.Entities;

namespace YetAnotherNoteTaker.Server.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<UserDto> CreateUser(NewUserDto newUserDto)
        {
            var current = await _usersRepository.GetByEmail(newUserDto.Email);
            if (current != null)
            {
                throw new InvalidParametersException(
                    nameof(newUserDto.Email), "Email is already in use.");
            }

            var user = new User { Email = newUserDto.Email, Password = EncryptionHelpers.Hash(newUserDto.Password) };
            user = await _usersRepository.Add(user);

            return new UserDto { Id = user.Id, Email = user.Email };
        }
    }
}
