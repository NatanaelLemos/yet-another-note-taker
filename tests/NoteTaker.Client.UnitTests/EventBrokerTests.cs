using System.Threading.Tasks;
using FluentAssertions;
using NoteTaker.Client.Events;
using Xunit;

namespace NoteTaker.Client.UnitTests
{
    public class EventBrokerTests
    {
        [Fact]
        public async Task ListenToCommandShouldCallCallback()
        {
            var broker = new EventBroker();

            var expectedResult = "expectedResult";
            var actualResult = "";
            broker.Listen<TestEvent>(t =>
            {
                actualResult = t.Prop;
                return Task.CompletedTask;
            });

            await broker.Command(new TestEvent { Prop = expectedResult });

            actualResult.Should().Be(expectedResult);
        }

        [Fact]
        public async Task ListToQueryShouldCallCallback()
        {
            var broker = new EventBroker();

            var expectedResult = "expected result";

            broker.Listen<TestEvent, string>(t =>
            {
                return Task.FromResult(expectedResult);
            });

            var actualResult = await broker.Query<TestEvent, string>(new TestEvent());

            actualResult.Should().Be(expectedResult);
        }

        private class TestEvent
        {
            public string Prop { get; set; }
        }
    }
}
