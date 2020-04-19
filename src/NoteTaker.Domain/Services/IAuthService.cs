using System.Threading.Tasks;
using NoteTaker.Domain.Dtos;

namespace NoteTaker.Domain.Services
{
    public interface IAuthService
    {
        Task<UserDto> GetByEmail(string email);
        Task<UserDto> GetByEmailAndPassword(string email, string password);
        Task CreateUser(NewUserDto userDto);
    }
}
