using Moq;
using Poetry.UI.ComponentSupport;
using Poetry.UI.ControllerSupport;
using Poetry.UI.ScriptSupport;
using Poetry.UI.StyleSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Poetry.UI.RoutingSupport.Tests
{
    public class ControllerRouterTests
    {
        [Fact]
        public void SupportsRootPath()
        {
            var basePathProvider = Mock.Of<IBasePathProvider>();

            new ControllerRouter(basePathProvider).Route("/");
        }

        [Fact]
        public void FindsAction()
        {
            var basePathProvider = Mock.Of<IBasePathProvider>();

            Mock.Get(basePathProvider).SetupGet(p => p.BasePath).Returns("basepath");

            var action = new ControllerAction("action", null);
            var controller = new Controller("controller", null, action);
            var component = new Component("component", null, Enumerable.Empty<string>(), new List<Controller> { controller }, Enumerable.Empty<Script>(), Enumerable.Empty<Style>());

            var result = new ControllerRouter(basePathProvider, component).Route("basepath/component/controller/action");

            Assert.NotNull(result);

            Assert.Same(component, result.Component);
            Assert.Same(controller, result.Controller);
            Assert.Same(action, result.Action);
        }

        [Fact]
        public void FindsActionCaseInsensitive()
        {
            var basePathProvider = Mock.Of<IBasePathProvider>();

            Mock.Get(basePathProvider).SetupGet(p => p.BasePath).Returns("Basepath");

            var action = new ControllerAction("Action", null);
            var controller = new Controller("Controller", null, action);
            var component = new Component("Component", null, Enumerable.Empty<string>(), new List<Controller> { controller }, Enumerable.Empty<Script>(), Enumerable.Empty<Style>());

            var result = new ControllerRouter(basePathProvider, component).Route("basepath/component/controller/action");

            Assert.NotNull(result);

            Assert.Same(component, result.Component);
            Assert.Same(controller, result.Controller);
            Assert.Same(action, result.Action);
        }

        [Fact]
        public void RequiresActionAsLastSegment()
        {
            var basePathProvider = Mock.Of<IBasePathProvider>();

            Mock.Get(basePathProvider).SetupGet(p => p.BasePath).Returns("basepath");

            var action = new ControllerAction("action", null);
            var controller = new Controller("controller", null, action);
            var component = new Component("component", null, Enumerable.Empty<string>(), new List<Controller> { controller }, Enumerable.Empty<Script>(), Enumerable.Empty<Style>());

            var result = new ControllerRouter(basePathProvider, component).Route("basepath/component/controller/action/something-extra");

            Assert.Null(result);
        }

        [Fact]
        public void MultiSegmentBasePath()
        {
            var basePathProvider = Mock.Of<IBasePathProvider>();

            Mock.Get(basePathProvider).SetupGet(p => p.BasePath).Returns("basepath1/basepath2");

            var action = new ControllerAction("action", null);
            var controller = new Controller("controller", null, action);
            var component = new Component("component", null, Enumerable.Empty<string>(), new List<Controller> { controller }, Enumerable.Empty<Script>(), Enumerable.Empty<Style>());

            var result = new ControllerRouter(basePathProvider, component).Route("basepath1/basepath2/component/controller/action");

            Assert.NotNull(result);

            Assert.Same(component, result.Component);
            Assert.Same(controller, result.Controller);
            Assert.Same(action, result.Action);
        }

        [Fact]
        public void MatchesBasePath()
        {
            var basePathProvider = Mock.Of<IBasePathProvider>();

            Mock.Get(basePathProvider).SetupGet(p => p.BasePath).Returns("basepath");

            var action = new ControllerAction("action", null);
            var controller = new Controller("controller", null, action);
            var component = new Component("component", null, Enumerable.Empty<string>(), new List<Controller> { controller }, Enumerable.Empty<Script>(), Enumerable.Empty<Style>());

            var result = new ControllerRouter(basePathProvider, component).Route("lorem/component/controller/action");

            Assert.Null(result);
        }

        [Fact]
        public void MatchesComponent()
        {
            var basePathProvider = Mock.Of<IBasePathProvider>();

            Mock.Get(basePathProvider).SetupGet(p => p.BasePath).Returns("basepath");

            var action = new ControllerAction("action", null);
            var controller = new Controller("controller", null, action);
            var component = new Component("component", null, Enumerable.Empty<string>(), new List<Controller> { controller }, Enumerable.Empty<Script>(), Enumerable.Empty<Style>());

            var result = new ControllerRouter(basePathProvider, component).Route("basepath/lorem/controller/action");

            Assert.Null(result);
        }

        [Fact]
        public void MatchesController()
        {
            var basePathProvider = Mock.Of<IBasePathProvider>();

            Mock.Get(basePathProvider).SetupGet(p => p.BasePath).Returns("basepath");

            var action = new ControllerAction("action", null);
            var controller = new Controller("controller", null, action);
            var component = new Component("component", null, Enumerable.Empty<string>(), new List<Controller> { controller }, Enumerable.Empty<Script>(), Enumerable.Empty<Style>());

            var result = new ControllerRouter(basePathProvider, component).Route("basepath/component/lorem/action");

            Assert.Null(result);
        }

        [Fact]
        public void MatchesAction()
        {
            var basePathProvider = Mock.Of<IBasePathProvider>();

            Mock.Get(basePathProvider).SetupGet(p => p.BasePath).Returns("basepath");

            var action = new ControllerAction("action", null);
            var controller = new Controller("controller", null, action);
            var component = new Component("component", null, Enumerable.Empty<string>(), new List<Controller> { controller }, Enumerable.Empty<Script>(), Enumerable.Empty<Style>());

            var result = new ControllerRouter(basePathProvider, component).Route("basepath/component/controller/lorem");

            Assert.Null(result);
        }
    }
}
