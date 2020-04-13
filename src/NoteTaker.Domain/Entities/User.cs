using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

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

        public void SetPassword(string password)
        {
            var data = Encoding.UTF8.GetBytes(password);
            using (var shaM = new SHA512Managed())
            {
                var hash = shaM.ComputeHash(data);
                Password = Encoding.UTF8.GetString(hash);
            }
        }
    }
}
