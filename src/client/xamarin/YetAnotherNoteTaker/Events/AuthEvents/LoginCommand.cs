using YetAnotherNoteTaker.State;

namespace YetAnotherNoteTaker.Events.AuthEvents
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
