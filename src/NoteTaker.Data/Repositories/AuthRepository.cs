using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NoteTaker.Domain.Data;
using NoteTaker.Domain.Entities;

namespace NoteTaker.Data.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly NoteTakerContext _ctx;

        public AuthRepository(NoteTakerContext ctx)
        {
            _ctx = ctx;
        }

        public Task<User> GetByEmail(string email)
        {
            return _ctx.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task Create(User user)
        {
            await _ctx.Users.AddAsync(user);
        }

        public Task Save()
        {
            return _ctx.SaveChangesAsync();
        }
    }
}
