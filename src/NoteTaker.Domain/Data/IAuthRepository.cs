using System.Threading.Tasks;
using NoteTaker.Domain.Entities;

namespace NoteTaker.Domain.Data
{
    public interface IAuthRepository
    {
        Task<User> GetByEmail(string email);

        Task Create(User user);

        Task Save();
    }
}
