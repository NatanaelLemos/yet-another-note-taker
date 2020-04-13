using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NoteTaker.Domain.Data;
using NoteTaker.Domain.Entities;

namespace NoteTaker.Data.Repositories
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly NoteTakerContext _ctx;

        public SettingsRepository(NoteTakerContext ctx)
        {
            _ctx = ctx;
        }

        public Task<Settings> GetByUserId(Guid userId)
        {
            return _ctx.Settings
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.UserId == userId);
        }

        public async Task CreateOrUpdate(Settings settings)
        {
            var dbSettings = await GetByUserId(settings.UserId);

            if (dbSettings == null)
            {
                _ctx.Settings.Add(settings);
            }
            else
            {
                _ctx.Update(settings);
            }
        }

        public Task Save()
        {
            return _ctx.SaveChangesAsync();
        }
    }
}
