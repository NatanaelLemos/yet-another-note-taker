using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Events.SettingsEvents
{
    public class SettingsRefreshResult
    {
        public SettingsRefreshResult(SettingsDto settings)
        {
            Settings = settings;
        }

        public SettingsDto Settings { get; }
    }
}
