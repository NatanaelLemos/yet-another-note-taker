using System.Threading.Tasks;
using NoteTaker.Client.Events;
using NoteTaker.Client.Events.AuthEvents;
using NoteTaker.Domain.Dtos;
using NoteTaker.Domain.Services;

namespace NoteTaker.Client.Services
{
    public class AuthAppService : IAuthAppService
    {
        private readonly IEventBroker _eventBroker;
        private readonly IAuthService _authService;

        public AuthAppService(IEventBroker eventBroker, IAuthService authService)
        {
            _eventBroker = eventBroker;
            _authService = authService;
        }

        public void StartListeners()
        {
            _eventBroker.Listen<CreateUserCommand>(CreateUserCommandHandler);
        }

        private Task CreateUserCommandHandler(CreateUserCommand arg)
        {
            return _authService.CreateUser(new UserDto
            {
                Email = arg.Email,
                Password = arg.Password
            });
        }
    }
}
