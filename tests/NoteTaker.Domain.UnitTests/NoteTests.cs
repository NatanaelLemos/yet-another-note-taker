using System;
using FluentAssertions;
using Xunit;

namespace NoteTaker.Domain.UnitTests
{
    public class NoteTests
    {
        [Fact]
        public void NewNoteShouldSetTheRightState()
        {
            var note = new Note();
            note.Id.Should().NotBeEmpty();
            note.Id.Should().NotBe(Guid.Empty);
            note.Available.Should().BeTrue();
            note.HistoryPosition.Should().Be(-1);
            note.History.Should().NotBeNull();
        }

        [Fact]
        public void NoteShouldAdvanceHistory()
        {
            var note = new Note();

            var firstState = "my message";
            note.CurrentState = firstState;
            note.HistoryPosition.Should().Be(0);
            note.CurrentState.Should().Be(firstState);

            var secondState = "my message has 2 states";
            note.CurrentState = secondState;
            note.HistoryPosition.Should().Be(1);
            note.CurrentState.Should().Be(secondState);

            note.History.Should().HaveCount(2);
            note.History[0].Should().Be(firstState);
            note.History[1].Should().Be(secondState);
        }

        [Fact]
        public void UndoShouldReturnOnePosition()
        {
            var note = new Note();

            var firstState = "my message";
            note.CurrentState = firstState;

            var secondState = "my message has 2 states";
            note.CurrentState = secondState;

            note.Undo();
            note.CurrentState.Should().Be(firstState);

            note.HistoryPosition.Should().Be(0);
            note.History.Should().HaveCount(2);
        }

        [Fact]
        public void UndoShouldNotGoBelowMinusOne()
        {
            var note = new Note();
            note.HistoryPosition.Should().Be(-1);
            note.Undo();
            note.HistoryPosition.Should().Be(-1);
        }

        [Fact]
        public void UndoShouldReturnToEmpty()
        {
            var note = new Note();

            var firstState = "my message";
            note.CurrentState = firstState;

            note.Undo();
            note.CurrentState.Should().Be(string.Empty);

            note.HistoryPosition.Should().Be(-1);
            note.History.Should().HaveCount(1);
        }

        [Fact]
        public void RedoShouldAdvanceState()
        {
            var note = new Note();

            var firstState = "my message";
            note.CurrentState = firstState;

            var secondState = "my second message";
            note.CurrentState = secondState;

            note.Undo();
            note.CurrentState.Should().Be(firstState);
            note.HistoryPosition.Should().Be(0);

            note.Redo();
            note.CurrentState.Should().Be(secondState);
            note.HistoryPosition.Should().Be(1);
        }

        [Fact]
        public void RedoShouldNotAdvanceMoreThanLimit()
        {
            var note = new Note();

            var firstState = "my message";
            note.CurrentState = firstState;

            var secondState = "my second message";
            note.CurrentState = secondState;

            note.Undo();
            note.Redo();
            note.Redo();
            note.Redo();
            note.CurrentState.Should().Be(secondState);
            note.HistoryPosition.Should().Be(1);
        }
    }
}
