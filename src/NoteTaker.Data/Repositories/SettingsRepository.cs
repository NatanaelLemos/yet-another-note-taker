using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task CreateOrUpdate(Settings settings)
        {
            var dbSettings = await Get();

            if (dbSettings == null)
            {
                _ctx.Settings.Add(settings);
            }
            else
            {
                _ctx.Update(settings);
            }
        }

        public async Task<Settings> Get()
        {
            var dbSettings = await _ctx.Settings.ToListAsync();
            return dbSettings.FirstOrDefault();
        }

        public Task Save()
        {
            return _ctx.SaveChangesAsync();
        }
    }
}
