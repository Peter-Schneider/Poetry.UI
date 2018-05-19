using Moq;
using Poetry.UI.ControllerSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Poetry.UI.ComponentSupport.Tests
{
    public class ComponentCreatorTests
    {
        [Fact]
        public void ThrowsOnNullType()
        {
            Assert.Throws<ArgumentNullException>(() => new ComponentCreator(null).Create(null));
        }

        [Fact]
        public void ThrowsIfTypeIsMissingComponentAttribute()
        {
            Assert.Throws<TypeIsMissingComponentAttributeException>(() => new ComponentCreator(null).Create(typeof(string)));
        }

        [Fact]
        public void CreatesComponent()
        {
            var result = new ComponentCreator(Mock.Of<IComponentControllerCreator>()).Create(typeof(MyComponentClass));

            Assert.NotNull(result);
            Assert.Equal("lorem-ipsum", result.Id);
        }

        [Fact]
        public void CallsControllerCreator()
        {
            var controller = new Controller("my-controller", null);

            var componentControllerCreator = Mock.Of<IComponentControllerCreator>();
            Mock.Get(componentControllerCreator).Setup(c => c.Create(typeof(MyComponentClass))).Returns(new List<Controller> { controller });

            var result = new ComponentCreator(componentControllerCreator).Create(typeof(MyComponentClass));

            Assert.NotNull(result);
            Assert.Equal("lorem-ipsum", result.Id);
            Assert.Same(typeof(MyComponentClass).Assembly, result.Assembly);
            Assert.Single(result.Controllers);
            Assert.Same(controller, result.Controllers.Single());
        }

        [Component("lorem-ipsum")]
        class MyComponentClass
        {

        }
    }
}
