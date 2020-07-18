using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace System.Nxlx.Observer.UnitTests
{
    public class ObserverExtensionsTests
    {
        [Fact]
        public void AddObserverShouldAddAnInstanceOfIEventBroker()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddObserver();
            serviceCollection.BuildServiceProvider()
                .GetService<IEventBroker>().Should().NotBeNull();
        }
    }
}
