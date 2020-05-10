using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using YetAnotherNoteTaker.Server.Entities;

namespace YetAnotherNoteTaker.Server.Data
{
    public class UsersRepository : IUsersRepository
    {
        private readonly NoteTakerContext _db;

        public UsersRepository(NoteTakerContext db)
        {
            _db = db;
        }

        public async Task<User> GetByEmail(string email)
        {
            var result = await _db.Users.FindAsync(u => u.Email == email);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<User> Add(User user)
        {
            await _db.Users.InsertOneAsync(user);
            return user;
        }

        public async Task<User> Update(User user)
        {
            user.Modified = DateTimeOffset.UtcNow;
            await _db.Users.FindOneAndReplaceAsync(u => u.Id == user.Id, user);
            return user;
        }
    }
}
