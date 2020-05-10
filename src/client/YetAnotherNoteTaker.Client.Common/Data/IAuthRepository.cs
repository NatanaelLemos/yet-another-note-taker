using System.Threading.Tasks;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Data
{
    public interface IAuthRepository
    {
        Task<UserDto> CreateUser(NewUserDto newUserDto);
        Task<string> GetAuthToken(string email, string password);
    }
}
