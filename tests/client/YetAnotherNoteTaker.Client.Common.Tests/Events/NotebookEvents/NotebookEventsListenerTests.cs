using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;
using YetAnotherNoteTaker.Client.Common.Events;
using YetAnotherNoteTaker.Client.Common.Events.NotebookEvents;
using YetAnotherNoteTaker.Client.Common.Services;
using Nxl.Observer;

namespace YetAnotherNoteTaker.Client.Common.UnitTests.Events.NotebookEvents
{
    public class NotebookEventsListenerTests
    {
        [Theory]
        [InlineData(false, true, "YetAnotherNoteTaker.Client.Common.Events.IEventBroker")]
        [InlineData(true, false, "YetAnotherNoteTaker.Client.Common.Services.INotebooksService")]
        public void ConstructorShouldThrowIfArgsAreNull(
            bool shouldMockEventBroker,
            bool shouldMockNotebooksService,
            string paramName)
        {
            var eventBroker = shouldMockEventBroker
                ? Mock.Of<IEventBroker>()
                : null;

            var notebooksService = shouldMockNotebooksService
                ? Mock.Of<INotebooksService>()
                : null;

            this.Invoking(_ => new NotebookEventsListener(
                eventBroker, notebooksService))
                .Should().Throw<ArgumentNullException>()
                .And.ParamName.Should().Be(paramName);
        }

        [Fact]
        public void StartShouldSubscribeToNotebookCommands()
        {
            var eventBroker = new Mock<IEventBroker>();

            var listener = new NotebookEventsListener(
                eventBroker.Object,
                Mock.Of<INotebooksService>());

            listener.Start();

            eventBroker.Verify(
                e => e.Subscribe(It.IsAny<Func<ListNotebooksCommand, Task>>()),
                Times.Once);

            eventBroker.Verify(
                e => e.Subscribe(It.IsAny<Func<EditNotebookCommand, Task>>()),
                Times.Once);

            eventBroker.Verify(
                e => e.Subscribe(It.IsAny<Func<DeleteNotebookCommand, Task>>()),
                Times.Once);

        }
    }
}
