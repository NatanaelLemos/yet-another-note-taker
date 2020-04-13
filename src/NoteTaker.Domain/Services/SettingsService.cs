using System;
using System.Threading.Tasks;
using NoteTaker.Domain.Data;
using NoteTaker.Domain.Dtos;
using NoteTaker.Domain.Entities;

namespace NoteTaker.Domain.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsRepository _repository;

        public SettingsService(ISettingsRepository repository)
        {
            _repository = repository;
        }

        public async Task<SettingsDto> GetByUserId(Guid userId)
        {
            var settings = await _repository.GetByUserId(userId);

            return new SettingsDto
            {
                Id = settings?.Id ?? Guid.NewGuid(),
                DarkMode = settings?.DarkMode ?? false
            };
        }

        public async Task CreateOrUpdateSettings(Guid userId, SettingsDto settings)
        {
            await _repository.CreateOrUpdate(new Settings
            {
                Id = settings.Id == Guid.Empty ? Guid.NewGuid() : settings.Id,
                DarkMode = settings.DarkMode,
                UserId = userId
            });
            await _repository.Save();
        }
    }
}
