using System.Threading.Tasks;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Server.Services
{
    public interface IUsersService
    {
        Task<UserDto> GetUserByEmail(string email);

        Task<bool> ValidatePassword(string email, string password);

        Task<UserDto> CreateUser(NewUserDto newUserDto);

        Task<UserDto> UpdateUser(string email, NewUserDto updatedUserDto);
    }
}
