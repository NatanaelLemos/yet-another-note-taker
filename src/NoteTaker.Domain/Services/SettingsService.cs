using System.Threading.Tasks;
using NoteTaker.Domain.Data;
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

        public async Task<Settings> Get()
        {
            var settings = await _repository.Get();

            if (settings == null)
            {
                return new Settings();
            }

            return settings;
        }

        public async Task CreateOrUpdateSettings(Settings settings)
        {
            await _repository.CreateOrUpdate(settings);
            await _repository.Save();
        }
    }
}
