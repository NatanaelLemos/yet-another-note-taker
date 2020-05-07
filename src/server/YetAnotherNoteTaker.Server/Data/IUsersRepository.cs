using System.Threading.Tasks;
using YetAnotherNoteTaker.Server.Entities;

namespace YetAnotherNoteTaker.Server.Data
{
    public interface IUsersRepository
    {
        public Task<User> GetByEmail(string email);

        public Task<User> Add(User user);
    }
}
