using System;
using NoteTaker.Client.State;
using Xunit;
using FluentAssertions;

namespace NoteTaker.Client.UnitTests
{
    public class EventBrokerTests
    {
        [Fact]
        public void ListenToCommandShouldCallCallback()
        {
            var broker = new EventBroker();

            var expectedResult = "expectedResult";
            var actualResult = "";
            broker.Listen<TestEvent>(t =>
            {
                actualResult = t.Prop;
            });

            broker.Command(new TestEvent { Prop = expectedResult });

            actualResult.Should().Be(expectedResult);
        }

        [Fact]
        public void ListToQueryShouldCallCallback()
        {
            var broker = new EventBroker();

            var expectedResult = "expected result";

            broker.Listen<TestEvent, string>(t =>
            {
                return expectedResult;
            });

            var actualResult = broker.Query<TestEvent, string>(new TestEvent());

            actualResult.Should().Be(expectedResult);
        }

        private class TestEvent
        {
            public string Prop { get; set; }
        }
    }
}
