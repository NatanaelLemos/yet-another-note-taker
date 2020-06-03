using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Events.SettingsEvents
{
    public class SettingsQueryResult
    {
        public SettingsQueryResult(SettingsDto settings)
        {
            Settings = settings;
        }

        public SettingsDto Settings { get; }
    }
}
