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
            container.RegisterSingleton<IEmbeddedResourceAssemblyCreator, EmbeddedResourceAssemblyCreator>();
            container.RegisterSingleton<IEmbeddedResourcePathMatcher, EmbeddedResourcePathMatcher>();
            container.RegisterSingleton<IEmbeddedResourceProvider, EmbeddedResourceProvider>();
        }
    }
}
