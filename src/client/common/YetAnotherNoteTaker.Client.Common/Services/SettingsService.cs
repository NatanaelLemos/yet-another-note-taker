using System;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Data;
using YetAnotherNoteTaker.Client.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Services
{
    public class SettingsService : ISettingsService
    {
        private static SettingsDto _settings = new SettingsDto
        {
            IsDarkMode = false
        };

        private readonly ISettingsRepository _repository;

        public SettingsService(ISettingsRepository repository)
        {
            _repository = repository;
        }

        public Task<SettingsDto> Get(Guid userId)
        {
            return Task.FromResult(_settings);
        }

        public Task Save(Guid userId, SettingsDto settings)
        {
            _settings = settings;
            return Task.CompletedTask;
        }
    }
}
