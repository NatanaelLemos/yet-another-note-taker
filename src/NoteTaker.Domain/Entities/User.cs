using System.Collections.Generic;

namespace NoteTaker.Domain.Entities
{
    public class User : EntityBase
    {
        public User()
        {
            Notebooks = new List<Notebook>();
            Settings = new Settings();
        }

        public string Email { get; set; }
        public string Password { get; set; }

        public IList<Notebook> Notebooks { get; set; }
        public Settings Settings { get; set; }
    }
}
