using NLemos.Api.Framework.Models;

namespace YetAnotherNoteTaker.Server.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
