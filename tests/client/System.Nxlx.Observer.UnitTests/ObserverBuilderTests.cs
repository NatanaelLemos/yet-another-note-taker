using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace System.Nxlx.Observer.UnitTests
{
    public class ObserverBuilderTests
    {
        [Fact]
        public void BuildShouldCreateAnInstanceOfIEventBroker()
        {
            var eventBroker = ObserverBuilder.Build();
            eventBroker.Should().NotBeNull();
        }
    }
}
