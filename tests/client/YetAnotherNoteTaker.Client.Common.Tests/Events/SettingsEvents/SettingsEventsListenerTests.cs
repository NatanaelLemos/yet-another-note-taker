using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;
using YetAnotherNoteTaker.Client.Common.Events;
using YetAnotherNoteTaker.Client.Common.Events.SettingsEvents;
using YetAnotherNoteTaker.Client.Common.Services;

namespace YetAnotherNoteTaker.Client.Common.UnitTests.Events.SettingsEvents
{
    public class SettingsEventsListenerTests
    {
        [Theory]
        [InlineData(false, true)]
        [InlineData(true, false)]
        public void ConstructorShouldThrowIfParametersAreNull(bool mockEventBroker, bool mockService)
        {
            var eventBroker = mockEventBroker ? Mock.Of<IEventBroker>() : null;
            var service = mockService ? Mock.Of<ISettingsService>() : null;

            this.Invoking(_ => new SettingsEventsListener(eventBroker, service))
                .Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void StartShouldListenToSettingsCommands()
        {
            var eventBroker = new Mock<IEventBroker>();
            var service = Mock.Of<ISettingsService>();

            var listener = new SettingsEventsListener(eventBroker.Object, service);
            listener.Start();

            eventBroker.Verify(
                e => e.Subscribe(It.IsAny<Func<SettingsQuery, Task>>()),
                Times.Once);

            eventBroker.Verify(
                e => e.Subscribe(It.IsAny<Func<EditSettingsCommand, Task>>()),
                Times.Once);

            eventBroker.Verify(
                e => e.Subscribe(It.IsAny<Func<SettingsRefreshQuery, Task>>()),
                Times.Once);
        }
    }
}
