using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Dtos;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Services
{
    public interface IAuthService
    {
        Task<UserDto> CreateUser(NewUserDto newUserDto);

        Task<LoggedInUserDto> Login(string email, string password);
    }
}
