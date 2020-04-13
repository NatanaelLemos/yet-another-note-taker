using System;
using NoteTaker.Domain.Dtos;

namespace NoteTaker.Client.Events.SettingsEvents
{
    public class CreateOrUpdateSettingsCommand
    {
        public CreateOrUpdateSettingsCommand(Guid userId, SettingsDto settings)
        {
            UserId = userId;
            Settings = settings;
        }

        public Guid UserId { get; set; }
        public SettingsDto Settings { get; }
    }
}
