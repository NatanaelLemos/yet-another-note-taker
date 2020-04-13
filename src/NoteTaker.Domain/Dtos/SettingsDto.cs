using System;

namespace NoteTaker.Domain.Dtos
{
    public class SettingsDto
    {
        public Guid Id { get; set; }
        public bool DarkMode { get; set; }
    }
}
