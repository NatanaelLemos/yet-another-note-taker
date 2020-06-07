using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Common.Dtos;
using YetAnotherNoteTaker.Server.Data;
using YetAnotherNoteTaker.Server.Entities;

namespace YetAnotherNoteTaker.Server.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsRepository _repository;

        public SettingsService(ISettingsRepository repository)
        {
            _repository = repository;
        }

        public async Task<SettingsDto> Get(string email)
        {
            var result = await _repository.Get(email);
            if (result == null)
            {
                return new SettingsDto();
            }

            return new SettingsDto
            {
                IsDarkMode = result.IsDarkMode
            };
        }

        public async Task<SettingsDto> Update(string email, SettingsDto settings)
        {
            var existing = await _repository.Get(email);

            if (existing == null)
            {
                await _repository.Add(new Settings
                {
                    UserEmail = email,
                    IsDarkMode = settings?.IsDarkMode ?? false
                });
            }
            else
            {
                existing.IsDarkMode = settings?.IsDarkMode ?? false;
                await _repository.Update(existing);
            }

            return settings;
        }
    }
}
