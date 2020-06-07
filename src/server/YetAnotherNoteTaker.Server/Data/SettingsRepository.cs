using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Server.Entities;
using MongoDB.Driver;

namespace YetAnotherNoteTaker.Server.Data
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly NoteTakerContext _ctx;

        public SettingsRepository(NoteTakerContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Settings> Get(string email)
        {
            var result = await _ctx.Settings.FindAsync(s => s.UserEmail == email);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<Settings> Add(Settings settings)
        {
            await _ctx.Settings.InsertOneAsync(settings);
            return settings;
        }

        public async Task<Settings> Update(Settings settings)
        {
            settings.Modified = DateTimeOffset.UtcNow;
            await _ctx.Settings.FindOneAndReplaceAsync(s => s.Id == settings.Id, settings);
            return settings;
        }
    }
}
