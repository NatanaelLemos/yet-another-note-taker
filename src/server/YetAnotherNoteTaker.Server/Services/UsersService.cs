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
        private readonly INotebooksRepository _notebooksRepository;
        private readonly INotesRepository _notesRepository;

        public UsersService(IUsersRepository usersRepository, INotebooksRepository notebooksRepository, INotesRepository notesRepository)
        {
            _usersRepository = usersRepository;
            _notebooksRepository = notebooksRepository;
            _notesRepository = notesRepository;
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            var user = await _usersRepository.GetByEmail(email);
            if (user == null)
            {
                return null;
            }

            return new UserDto { Email = user.Email };
        }

        public async Task<bool> ValidatePassword(string email, string password)
        {
            var user = await _usersRepository.GetByEmail(email);
            if (user == null)
            {
                return false;
            }

            return EncryptionHelpers.Hash(password).Equals(user.Password);
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

            return new UserDto { Email = user.Email };
        }

        public async Task<UserDto> UpdateUser(string email, NewUserDto updatedUserDto)
        {
            var current = await _usersRepository.GetByEmail(updatedUserDto.Email);
            if (current != null)
            {
                throw new InvalidParametersException(
                    nameof(updatedUserDto.Email), "Email is already in use.");
            }

            current = await _usersRepository.GetByEmail(email);
            current.Email = updatedUserDto.Email;
            current.Password = EncryptionHelpers.Hash(updatedUserDto.Password);

            current = await _usersRepository.Update(current);

            await _notebooksRepository.UpdateEmails(email, current.Email);
            await _notesRepository.UpdateEmails(email, current.Email);


            return new UserDto { Email = current.Email };
        }
    }
}
