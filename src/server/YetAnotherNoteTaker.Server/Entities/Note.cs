using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLemos.Api.Framework.Models;

namespace YetAnotherNoteTaker.Server.Entities
{
    public class Note : BaseEntity
    {
        public string Key { get; set; }
        public string NotebookKey { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
    }
}
