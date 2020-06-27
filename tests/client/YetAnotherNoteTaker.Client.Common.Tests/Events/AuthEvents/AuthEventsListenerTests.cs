using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;
using YetAnotherNoteTaker.Client.Common.Events;
using YetAnotherNoteTaker.Client.Common.Events.AuthEvents;
using YetAnotherNoteTaker.Client.Common.Services;

namespace YetAnotherNoteTaker.Client.Common.Tests.Events.AuthEvents
{
    public class AuthEventsListenerTests
    {
        [Theory]
        [InlineData(false, true, "YetAnotherNoteTaker.Client.Common.Events.IEventBroker")]
        [InlineData(true, false, "YetAnotherNoteTaker.Client.Common.Services.IAuthService")]
        public void ConstructorShouldThrowIfArgsAreNull(
            bool shouldMockEventBroker,
            bool shouldMockAuthService,
            string paramName)
        {
            var eventBroker = shouldMockEventBroker
                ? Mock.Of<IEventBroker>()
                : null;

            var authService = shouldMockAuthService
                ? Mock.Of<IAuthService>()
                : null;

            this.Invoking(_ => new AuthEventsListener(eventBroker, authService))
                .Should().Throw<ArgumentNullException>()
                .And.ParamName.Should().Be(paramName);
        }

        [Fact]
        public void StartShouldSubscribeToAuthCommands()
        {
            var eventBroker = new Mock<IEventBroker>();

            var listener = new AuthEventsListener(
                eventBroker.Object,
                Mock.Of<IAuthService>());

            listener.Start();

            eventBroker.Verify(
                e => e.Subscribe(It.IsAny<Func<CreateUserCommand, Task>>()),
                Times.Once);
            eventBroker.Verify(
                e => e.Subscribe(It.IsAny<Func<LoginCommand, Task>>()),
                Times.Once);
        }
    }
}
