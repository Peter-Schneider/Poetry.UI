using Moq;
using Poetry.UI.ControllerSupport;
using System;
using System.Collections.Generic;
using Xunit;

namespace Poetry.UI.ComponentSupport.Tests
{
    public class ComponentCreatorTests
    {
        [Fact]
        public void CreatesComponent()
        {
            var result = new ComponentCreator(Mock.Of<IControllerCreator>()).Create(typeof(MyComponentClass));

            Assert.NotNull(result);
            Assert.Equal("lorem-ipsum", result.Id);
        }

        [Component("lorem-ipsum")]
        class MyComponentClass
        {

        }
    }
}
