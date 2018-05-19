using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Poetry.UI.ControllerSupport.Tests
{
    public class ControllerActionCreatorTests
    {
        [Fact]
        public void CreatesControllerActions()
        {
            var result = new ControllerActionCreator().Create(typeof(MyController));

            Assert.Single(result);
            Assert.Equal("a", result.Single().Id);
            Assert.Equal(typeof(MyController).GetMethod(nameof(MyController.Lorem)), result.Single().Method);
        }

        public class MyController
        {
            [Action("a")]
            public void Lorem()
            {

            }

            public void Ipsum()
            {

            }
        }
    }
}
