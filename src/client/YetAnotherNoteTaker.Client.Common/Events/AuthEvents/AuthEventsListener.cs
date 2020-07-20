using System.Threading.Tasks;
using N2tl.Observer;
using YetAnotherNoteTaker.Client.Common.Services;
using YetAnotherNoteTaker.Common.Dtos;
using YetAnotherNoteTaker.Common.Helpers;

namespace YetAnotherNoteTaker.Client.Common.Events.AuthEvents
{
    public class AuthEventsListener
    {
        private readonly IEventBroker _eventBroker;
        private readonly IAuthService _authService;

        public AuthEventsListener(IEventBroker eventBroker, IAuthService authService)
        {
            _eventBroker = eventBroker.AsNotNull();
            _authService = authService.AsNotNull();
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

        private Task LoginCommandHandler(LoginCommand arg)
        {
            return _authService.Login(arg.Email, arg.Password);
        }
    }
}
