using Moq;
using Poetry.UI.ControllerSupport;
using Poetry.UI.ScriptSupport;
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
            Assert.Throws<ArgumentNullException>(() => new ComponentCreator(null, null).Create(null));
        }

        [Fact]
        public void ThrowsIfTypeIsMissingComponentAttribute()
        {
            Assert.Throws<TypeIsMissingComponentAttributeException>(() => new ComponentCreator(null, null).Create(typeof(string)));
        }

        [Fact]
        public void CreatesComponent()
        {
            var result = new ComponentCreator(Mock.Of<IComponentControllerCreator>(), Mock.Of<IScriptCreator>()).Create(typeof(MyComponentClass));

            Assert.NotNull(result);
            Assert.Equal("lorem-ipsum", result.Id);
        }

        [Fact]
        public void CallsControllerCreator()
        {
            var controller = new Controller("my-controller", null);

            var componentControllerCreator = Mock.Of<IComponentControllerCreator>();
            Mock.Get(componentControllerCreator).Setup(c => c.Create(typeof(MyComponentClass))).Returns(new List<Controller> { controller });

            var result = new ComponentCreator(componentControllerCreator, Mock.Of<IScriptCreator>()).Create(typeof(MyComponentClass));

            Assert.Equal("lorem-ipsum", result.Id);
            Assert.Same(typeof(MyComponentClass).Assembly, result.Assembly);
            Assert.Single(result.Controllers);
            Assert.Same(controller, result.Controllers.Single());
        }

        [Fact]
        public void CallsScriptCreator()
        {
            var script = new Script("my-controller");

            var scriptCreator = Mock.Of<IScriptCreator>();
            Mock.Get(scriptCreator).Setup(c => c.Create(typeof(MyComponentClass))).Returns(new List<Script> { script });

            var result = new ComponentCreator(Mock.Of<IComponentControllerCreator>(), scriptCreator).Create(typeof(MyComponentClass));

            Assert.Single(result.Scripts);
            Assert.Same(script, result.Scripts.Single());
        }

        [Component("lorem-ipsum")]
        class MyComponentClass
        {

        }
    }
}
