using System;
using System.Collections.Generic;
using System.Text;
using YetAnotherNoteTaker.State;

namespace YetAnotherNoteTaker.Events.AuthEvents
{
    [AllowAnonymous]
    public class CreateUserCommand
    {
        public CreateUserCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public string Password { get; }
    }
}
