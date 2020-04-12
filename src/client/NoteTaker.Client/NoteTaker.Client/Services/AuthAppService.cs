using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NoteTaker.Client.Events;
using NoteTaker.Client.Events.AuthEvents;

namespace NoteTaker.Client.Services
{
    public class AuthAppService : IAuthAppService
    {
        private readonly IEventBroker _eventBroker;

        public AuthAppService(IEventBroker eventBroker)
        {
            _eventBroker = eventBroker;
        }

        public void StartListeners()
        {
            _eventBroker.Listen<CreateUserCommand>(CreateUserCommandHandler);
        }

        private Task CreateUserCommandHandler(CreateUserCommand arg)
        {
            throw new NotImplementedException();
        }
    }
}
