using System.Threading.Tasks;
using YetAnotherNoteTaker.Common.Dtos;
using YetAnotherNoteTaker.Client.Common.Services;
using YetAnotherNoteTaker.State;

namespace YetAnotherNoteTaker.Events.SettingsEvents
{
    public class SettingsEventsListener
    {
        private readonly IEventBroker _eventBroker;
        private readonly ISettingsService _service;

        public SettingsEventsListener(IEventBroker eventBroker, ISettingsService service)
        {
            _eventBroker = eventBroker;
            _service = service;
        }

        public void Start()
        {
            _eventBroker.Subscribe<SettingsQuery>(SettingsQueryHandler);
            _eventBroker.Subscribe<EditSettingsCommand>(EditSettingsCommandHandler);
            _eventBroker.Subscribe<SettingsRefreshQuery>(SettingsRefreshQueryHandler);
        }

        private async Task SettingsQueryHandler(SettingsQuery arg)
        {
            var settings = await _service.Get(UserState.UserId);
            await _eventBroker.Notify(new SettingsQueryResult(settings));
        }

        private Task EditSettingsCommandHandler(EditSettingsCommand arg)
        {
            var settings = new SettingsDto { IsDarkMode = arg.IsDarkMode };
            return _service.Save(UserState.UserId, settings);
        }

        private async Task SettingsRefreshQueryHandler(SettingsRefreshQuery arg)
        {
            var settings = await _service.Get(UserState.UserId);
            await _eventBroker.Notify(new SettingsRefreshResult(settings));
        }
    }
}
