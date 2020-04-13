using System.Threading.Tasks;
using NoteTaker.Client.Events;
using NoteTaker.Client.Events.SettingsEvents;
using NoteTaker.Domain.Dtos;
using NoteTaker.Domain.Services;

namespace NoteTaker.Client.Services
{
    public class SettingsAppService : ISettingsAppService
    {
        private readonly IEventBroker _eventBroker;
        private readonly ISettingsService _settingsService;

        private SettingsDto _cache;

        public SettingsAppService(IEventBroker eventBroker, ISettingsService settingsService)
        {
            _eventBroker = eventBroker;
            _settingsService = settingsService;
        }

        public void StartListeners()
        {
            _eventBroker.Listen<CreateOrUpdateSettingsCommand>(CreateOrUpdateSettingsCommandHandler);
            _eventBroker.Listen<SettingsQuery, SettingsDto>(SettingsQuery);
        }

        public Task CreateOrUpdateSettingsCommandHandler(CreateOrUpdateSettingsCommand command)
        {
            _cache = command.Settings;
            return _settingsService.CreateOrUpdateSettings(command.UserId, command.Settings);
        }

        public async Task<SettingsDto> SettingsQuery(SettingsQuery query)
        {
            if (_cache == null)
            {
                _cache = await _settingsService.GetByUserId(query.UserId);
            }

            return _cache;
        }
    }
}
