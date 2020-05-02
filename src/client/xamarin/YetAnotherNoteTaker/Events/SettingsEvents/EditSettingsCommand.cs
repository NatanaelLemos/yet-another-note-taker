using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherNoteTaker.Events.SettingsEvents
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
