using Poetry.UI.DependencyInjectionSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.EmbeddedResourceSupport
{
    public class EmbeddedResourceSupportDependencyInjector : IDependencyInjector
    {
        public void InjectDependencies(IContainer container)
        {
            container.RegisterType<IEmbeddedResourceAssemblyCreator, EmbeddedResourceAssemblyCreator>();
            container.RegisterType<IEmbeddedResourceAssemblyProvider, EmbeddedResourceAssemblyProvider>();
            container.RegisterType<IEmbeddedResourcePathMatcher, EmbeddedResourcePathMatcher>();
            container.RegisterType<IEmbeddedResourceProvider, EmbeddedResourceProvider>();
        }
    }
}
