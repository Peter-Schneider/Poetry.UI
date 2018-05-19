using Moq;
using Poetry.UI.ControllerSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Poetry.UI.ComponentSupport.Tests
{
    public class ComponentControllerCreatorTests
    {
        [Fact]
        public void UsesComponentControllerTypeProvider()
        {
            var componentControllerTypeProvider = Mock.Of<IComponentControllerTypeProvider>();

            Mock.Get(componentControllerTypeProvider).Setup(p => p.GetTypes(typeof(string))).Returns(new List<Type> { typeof(MyControllerType) });

            var controllerCreator = Mock.Of<IControllerCreator>();

            var controller = new Controller("id", null);

            Mock.Get(controllerCreator).Setup(c => c.Create(typeof(MyControllerType))).Returns(controller);

            var result = new ComponentControllerCreator(componentControllerTypeProvider, controllerCreator).Create(typeof(string));

            Assert.Single(result);
            Assert.Same(controller, result.Single());
        }

        public class MyControllerType { }
    }
}
