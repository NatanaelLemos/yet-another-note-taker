using NLemos.Api.Framework.Models;

namespace YetAnotherNoteTaker.Server.Entities
{
    public class Notebook : BaseEntity
    {
        public string Key { get; set; }
        public string UserEmail { get; set; }
        public string Name { get; set; }
    }
}
