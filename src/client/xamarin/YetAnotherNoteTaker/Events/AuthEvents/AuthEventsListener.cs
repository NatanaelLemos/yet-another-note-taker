﻿using System.Threading.Tasks;
using YetAnotherNoteTaker.Common.Dtos;
using YetAnotherNoteTaker.Client.Common.Services;
using YetAnotherNoteTaker.State;

namespace YetAnotherNoteTaker.Events.AuthEvents
{
    public class AuthEventsListener
    {
        private readonly IEventBroker _eventBroker;
        private readonly IAuthService _authService;

        public AuthEventsListener(IEventBroker eventBroker, IAuthService authService)
        {
            _eventBroker = eventBroker;
            _authService = authService;
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
            UserState.SetUser(user);
        }
    }
}
