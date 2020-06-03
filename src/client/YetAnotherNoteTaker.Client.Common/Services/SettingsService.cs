using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Data;
using YetAnotherNoteTaker.Common.Dtos;

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

        public Task<SettingsDto> Get()
        {
            return Task.FromResult(_settings);
        }

        public Task Save(SettingsDto settings)
        {
            _settings = settings;
            return Task.CompletedTask;
        }
    }
}
