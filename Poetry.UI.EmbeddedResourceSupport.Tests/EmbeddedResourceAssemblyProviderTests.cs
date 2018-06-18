using Moq;
using Poetry.UI.AppSupport;
using Poetry.UI.ComponentSupport;
using Poetry.UI.ControllerSupport;
using Poetry.UI.ReflectionSupport;
using Poetry.UI.ScriptSupport;
using Poetry.UI.StyleSupport;
using Poetry.UI.TranslationSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Poetry.UI.EmbeddedResourceSupport.Tests
{
    public class EmbeddedResourceAssemblyProviderTests
    {
        [Fact]
        public void UsesComponentRepository()
        {
            var component = new Component("lorem", new AssemblyWrapper(new List<Type> { }), Enumerable.Empty<string>(), Enumerable.Empty<Controller>(), Enumerable.Empty<Script>(), Enumerable.Empty<Style>());

            var componentRepository = Mock.Of<IComponentRepository>();

            Mock.Get(componentRepository).Setup(r => r.GetAll()).Returns(new List<Component> { component });

            var embeddedResourceAssemblyCreator = Mock.Of<IEmbeddedResourceAssemblyCreator>();

            var embeddedResourceAssembly = new EmbeddedResourceAssembly("ipsum", "basepath", null, Enumerable.Empty<EmbeddedResource>());

            Mock.Get(embeddedResourceAssemblyCreator).Setup(c => c.Create(component.Id, component.Assembly)).Returns(embeddedResourceAssembly);

            var result = new EmbeddedResourceAssemblyProvider(componentRepository, Mock.Of<IAppRepository>(), embeddedResourceAssemblyCreator).GetAll();

            Assert.Single(result, embeddedResourceAssembly);
        }

        [Fact]
        public void UsesAppRepository()
        {
            var app = new App("lorem", new AssemblyWrapper(new List<Type> { }), Enumerable.Empty<Script>(), Enumerable.Empty<Style>(), Mock.Of<ITranslationRepository>());

            var appRepository = Mock.Of<IAppRepository>();

            Mock.Get(appRepository).Setup(r => r.GetAll()).Returns(new List<App> { app });

            var embeddedResourceAssemblyCreator = Mock.Of<IEmbeddedResourceAssemblyCreator>();

            var embeddedResourceAssembly = new EmbeddedResourceAssembly("ipsum", "basepath", null, Enumerable.Empty<EmbeddedResource>());

            Mock.Get(embeddedResourceAssemblyCreator).Setup(c => c.Create(app.Name, app.Assembly)).Returns(embeddedResourceAssembly);

            var result = new EmbeddedResourceAssemblyProvider(Mock.Of<IComponentRepository>(), appRepository, embeddedResourceAssemblyCreator).GetAll();

            Assert.Single(result, embeddedResourceAssembly);
        }
    }
}
