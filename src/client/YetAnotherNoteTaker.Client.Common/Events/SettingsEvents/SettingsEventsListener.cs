using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Services;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Events.SettingsEvents
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
            var settings = await _service.Get();
            await _eventBroker.Notify(new SettingsQueryResult(settings));
        }

        private async Task EditSettingsCommandHandler(EditSettingsCommand arg)
        {
            var settings = new SettingsDto { IsDarkMode = arg.IsDarkMode };
            await _service.Save(settings);
        }

        private async Task SettingsRefreshQueryHandler(SettingsRefreshQuery arg)
        {
            var settings = await _service.Get();
            await _eventBroker.Notify(new SettingsRefreshResult(settings));
        }
    }
}
