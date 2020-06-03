namespace YetAnotherNoteTaker.Client.Common.Events.SettingsEvents
{
    public class EditSettingsCommand
    {
        public EditSettingsCommand(bool isDarkMode)
        {
            IsDarkMode = isDarkMode;
        }

        public bool IsDarkMode { get; }
    }
}
