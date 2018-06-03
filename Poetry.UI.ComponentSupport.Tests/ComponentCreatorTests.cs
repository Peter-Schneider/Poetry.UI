using Moq;
using Poetry.UI.ComponentSupport.DependencySupport;
using Poetry.UI.ControllerSupport;
using Poetry.UI.ScriptSupport;
using Poetry.UI.StyleSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Poetry.UI.ComponentSupport.Tests
{
    public class ComponentCreatorTests
    {
        [Fact]
        public void CreatesComponent()
        {
            var componentTypeProvider = Mock.Of<IComponentTypeProvider>();

            Mock.Get(componentTypeProvider).Setup(p => p.GetTypes()).Returns(new List<Type> { typeof(MyComponentClass) });

            var result = new ComponentCreator(componentTypeProvider, Mock.Of<IComponentDependencyCreator>(), Mock.Of<IComponentControllerCreator>(), Mock.Of<IScriptCreator>(), Mock.Of<IStyleCreator>()).Create().Single();

            Assert.NotNull(result);
            Assert.Equal("lorem-ipsum", result.Id);
        }

        [Fact]
        public void CallsControllerCreator()
        {
            var componentTypeProvider = Mock.Of<IComponentTypeProvider>();

            Mock.Get(componentTypeProvider).Setup(p => p.GetTypes()).Returns(new List<Type> { typeof(MyComponentClass) });

            var controller = new Controller("my-controller", null);

            var componentControllerCreator = Mock.Of<IComponentControllerCreator>();
            Mock.Get(componentControllerCreator).Setup(c => c.Create(typeof(MyComponentClass))).Returns(new List<Controller> { controller });

            var result = new ComponentCreator(componentTypeProvider, Mock.Of<IComponentDependencyCreator>(), componentControllerCreator, Mock.Of<IScriptCreator>(), Mock.Of<IStyleCreator>()).Create().Single();

            Assert.Equal("lorem-ipsum", result.Id);
            Assert.Same(typeof(MyComponentClass).Assembly, result.Assembly);
            Assert.Single(result.Controllers);
            Assert.Same(controller, result.Controllers.Single());
        }

        [Fact]
        public void CallsScriptCreator()
        {
            var componentTypeProvider = Mock.Of<IComponentTypeProvider>();

            Mock.Get(componentTypeProvider).Setup(p => p.GetTypes()).Returns(new List<Type> { typeof(MyComponentClass) });

            var script = new Script("my-controller");

            var scriptCreator = Mock.Of<IScriptCreator>();
            Mock.Get(scriptCreator).Setup(c => c.Create(typeof(MyComponentClass))).Returns(new List<Script> { script });

            var result = new ComponentCreator(componentTypeProvider, Mock.Of<IComponentDependencyCreator>(), Mock.Of<IComponentControllerCreator>(), scriptCreator, Mock.Of<IStyleCreator>()).Create().Single();

            Assert.Single(result.Scripts);
            Assert.Same(script, result.Scripts.Single());
        }

        [Fact]
        public void CallsStyleCreator()
        {
            var componentTypeProvider = Mock.Of<IComponentTypeProvider>();

            Mock.Get(componentTypeProvider).Setup(p => p.GetTypes()).Returns(new List<Type> { typeof(MyComponentClass) });

            var style = new Style("my-controller");

            var styleCreator = Mock.Of<IStyleCreator>();
            Mock.Get(styleCreator).Setup(c => c.Create(typeof(MyComponentClass))).Returns(new List<Style> { style });

            var result = new ComponentCreator(componentTypeProvider, Mock.Of<IComponentDependencyCreator>(), Mock.Of<IComponentControllerCreator>(), Mock.Of<IScriptCreator>(), styleCreator).Create().Single();

            Assert.Single(result.Styles);
            Assert.Same(style, result.Styles.Single());
        }
    }
}
