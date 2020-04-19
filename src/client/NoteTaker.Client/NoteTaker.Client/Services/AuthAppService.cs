using System;
using System.Threading.Tasks;
using NoteTaker.Client.Events;
using NoteTaker.Client.Events.AuthEvents;
using NoteTaker.Client.State;
using NoteTaker.Domain.Dtos;
using NoteTaker.Domain.Helpers;
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
            _eventBroker.Listen<LoginCommand>(LoginCommandHandler);
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
            var currentUser = await _authService.GetByEmailAndPassword(arg.Email, arg.Password);
            if (currentUser == null)
            {
                await _eventBroker.Command(new LoginErrorCommand());
            }
            else
            {
                UserState.CurrentUser = currentUser;
                await _eventBroker.Command(new UserLoggedInCommand(currentUser));
            }
        }
    }
}
