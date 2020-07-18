using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace System.Nxlx.Observer.UnitTests
{
    public class ObserverOptionsTests
    {
        [Fact]
        public void AddInterrupterShouldAddInterrupters()
        {
            var options = new ObserverOptions();
            options.AddInterrupter(t => Task.FromResult(true));
            options.Interrupters.Should().HaveCount(1);
        }
    }
}
