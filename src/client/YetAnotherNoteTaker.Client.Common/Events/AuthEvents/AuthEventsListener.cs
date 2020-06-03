using System.Threading.Tasks;
using YetAnotherNoteTaker.Common.Dtos;
using YetAnotherNoteTaker.Client.Common.Services;
using YetAnotherNoteTaker.Client.Common.State;

namespace YetAnotherNoteTaker.Client.Common.Events.AuthEvents
{
    public class AuthEventsListener
    {
        private readonly IEventBroker _eventBroker;
        private readonly IAuthService _authService;
        private readonly IUserState _userState;

        public AuthEventsListener(IEventBroker eventBroker, IAuthService authService, IUserState userState)
        {
            _eventBroker = eventBroker;
            _authService = authService;
            _userState = userState;
        }

        public void Start()
        {
            _eventBroker.Subscribe<CreateUserCommand>(CreateUserCommandHandler);
            _eventBroker.Subscribe<LoginCommand>(LoginCommandHandler);
        }

        private Task CreateUserCommandHandler(CreateUserCommand arg)
        {
            return _authService.CreateUser(new NewUserDto
            {
                Email = arg.Email,
                Password = arg.Password
            });
        }

        private async Task LoginCommandHandler(LoginCommand arg)
        {
            var user = await _authService.Login(arg.Email, arg.Password);
            await _userState.SetUser(user);
        }
    }
}
