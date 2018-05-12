using System;
using Xunit;

namespace Poetry.UI.ComponentSupport.Tests
{
    public class ComponentCreatorTests
    {
        [Fact]
        public void Works()
        {
            Assert.NotNull(new ComponentCreator(null).Create(typeof(string)));
        }
    }
}
