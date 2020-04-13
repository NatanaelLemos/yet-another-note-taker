using System;

namespace NoteTaker.Client.Events.SettingsEvents
{
    public class SettingsQuery
    {
        public SettingsQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }
}
