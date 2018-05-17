using Moq;
using Poetry.UI.MvcSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Poetry.UI.ControllerSupport.Tests
{
    public class ControllerCreatorTests
    {
        [Fact]
        public void CreatesController()
        {
            var controllerActionCreator = Mock.Of<IControllerActionCreator>();

            var controllerAction = new ControllerAction("test-action");

            Mock.Get(controllerActionCreator).Setup(c => c.Create(typeof(MyController))).Returns(new List<ControllerAction> { controllerAction });

            var result = new ControllerCreator(controllerActionCreator).Create(typeof(MyController));

            Assert.NotNull(result);
            Assert.Equal("a", result.Id);
            Assert.Single(result.Actions);
            Assert.Same(controllerAction, result.Actions.Single());
        }

        [Controller("a")]
        public class MyController
        {

        }
    }
}
