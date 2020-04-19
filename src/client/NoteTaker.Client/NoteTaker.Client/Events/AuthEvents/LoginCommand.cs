using System;
using System.Collections.Generic;
using System.Text;
using NoteTaker.Client.State;

namespace NoteTaker.Client.Events.AuthEvents
{
    [AllowAnonymous]
    public class LoginCommand
    {
        public LoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public string Password { get; }
    }
}
