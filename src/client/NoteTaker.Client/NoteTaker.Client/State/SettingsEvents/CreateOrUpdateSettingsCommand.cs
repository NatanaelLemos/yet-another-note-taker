using System;
using System.Collections.Generic;
using System.Text;
using NoteTaker.Domain.Entities;

namespace NoteTaker.Client.State.SettingsEvents
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
