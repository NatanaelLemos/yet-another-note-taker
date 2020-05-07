using System.Threading.Tasks;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Server.Services
{
    public interface IUsersService
    {
        Task<UserDto> CreateUser(NewUserDto newUserDto);
    }
}
