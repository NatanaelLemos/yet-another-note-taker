using System.Threading.Tasks;
using NoteTaker.Client.State;
using NoteTaker.Client.State.SettingsEvents;
using NoteTaker.Domain.Entities;
using NoteTaker.Domain.Services;

namespace NoteTaker.Client.Services
{
    public class SettingsAppService : ISettingsAppService
    {
        private readonly IEventBroker _eventBroker;
        private readonly ISettingsService _settingsService;

        private Settings _cache;

        public SettingsAppService(IEventBroker eventBroker, ISettingsService settingsService)
        {
            _eventBroker = eventBroker;
            _settingsService = settingsService;
        }

        public void StartListeners()
        {
            _eventBroker.Listen<CreateOrUpdateSettingsCommand>(CreateOrUpdateSettingsCommandHandler);
            _eventBroker.Listen<SettingsQuery, Settings>(SettingsQuery);
        }

        public Task CreateOrUpdateSettingsCommandHandler(CreateOrUpdateSettingsCommand command)
        {
            _cache = command.Settings;
            return _settingsService.CreateOrUpdateSettings(command.Settings);
        }

        public async Task<Settings> SettingsQuery(SettingsQuery query)
        {
            if (_cache == null)
            {
                _cache = await _settingsService.Get();
            }

            return _cache;
        }
    }
}
