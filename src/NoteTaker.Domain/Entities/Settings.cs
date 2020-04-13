using System;

namespace NoteTaker.Domain.Entities
{
    public class Settings : EntityBase
    {
        public bool DarkMode { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
