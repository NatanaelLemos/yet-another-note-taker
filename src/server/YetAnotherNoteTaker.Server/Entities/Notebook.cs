using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLemos.Api.Framework.Models;

namespace YetAnotherNoteTaker.Server.Entities
{
    public class Notebook : BaseEntity
    {
        public string UserEmail { get; set; }
        public string Name { get; set; }
    }
}
