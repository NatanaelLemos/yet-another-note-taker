using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLemos.Api.Framework.Models;

namespace YetAnotherNoteTaker.Server.Entities
{
    public class Settings : BaseEntity
    {
        public string UserEmail { get; set; }
        public bool IsDarkMode { get; set; }
    }
}
