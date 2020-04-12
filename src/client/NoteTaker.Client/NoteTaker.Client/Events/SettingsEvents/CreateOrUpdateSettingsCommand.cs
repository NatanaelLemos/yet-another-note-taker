using NoteTaker.Domain.Entities;

namespace NoteTaker.Client.Events.SettingsEvents
{
    public class CreateOrUpdateSettingsCommand
    {
        public CreateOrUpdateSettingsCommand(Settings settings)
        {
            Settings = settings;
        }

        public Settings Settings { get; }
    }
}
