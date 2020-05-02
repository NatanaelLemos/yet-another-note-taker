using YetAnotherNoteTaker.Client.Common.Dtos;

namespace YetAnotherNoteTaker.Events.SettingsEvents
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
