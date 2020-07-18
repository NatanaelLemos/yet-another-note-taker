using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;
using YetAnotherNoteTaker.Client.Common.Events;
using YetAnotherNoteTaker.Client.Common.Events.NoteEvents;
using YetAnotherNoteTaker.Client.Common.Services;
using Observatron;

namespace YetAnotherNoteTaker.Client.Common.UnitTests.Events.NoteEvents
{
    public class NoteEventsListenerTests
    {
        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void ConstructorShouldThrowIfParamsAreNull(bool mockEventBroker, bool mockService)
        {
            var eventBroker = mockEventBroker ? Mock.Of<IEventBroker>() : null;
            var service = mockService ? Mock.Of<INotesService>() : null;

            this.Invoking(_ => new NoteEventsListener(eventBroker, service))
                .Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void StartShouldListenToNotesCommands()
        {
            var eventBroker = new Mock<IEventBroker>();
            var service = new Mock<INotesService>();

            var listener = new NoteEventsListener(eventBroker.Object, service.Object);
            listener.Start();

            eventBroker.Verify(
                e => e.Subscribe(It.IsAny<Func<ListNotesCommand, Task>>()),
                Times.Once);

            eventBroker.Verify(
                e => e.Subscribe(It.IsAny<Func<EditNoteCommand, Task>>()),
                Times.Once);

            eventBroker.Verify(
                e => e.Subscribe(It.IsAny<Func<DeleteNoteCommand, Task>>()),
                Times.Once);
        }
    }
}
