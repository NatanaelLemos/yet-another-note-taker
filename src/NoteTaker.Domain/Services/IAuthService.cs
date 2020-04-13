using System.Threading.Tasks;
using NoteTaker.Domain.Dtos;

namespace NoteTaker.Domain.Services
{
    public interface IAuthService
    {
        Task CreateUser(UserDto userDto);
    }
}
