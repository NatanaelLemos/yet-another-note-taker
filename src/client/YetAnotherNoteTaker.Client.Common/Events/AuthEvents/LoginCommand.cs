using YetAnotherNoteTaker.Client.Common.Security;

namespace YetAnotherNoteTaker.Client.Common.Events.AuthEvents
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
