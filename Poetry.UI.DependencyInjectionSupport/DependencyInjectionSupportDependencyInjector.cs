using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.DependencyInjectionSupport
{
    public class DependencyInjectionSupportDependencyInjector : IDependencyInjector
    {
        public void InjectDependencies(IContainer container)
        {
            container.RegisterSingleton<IDependencyInjectorTypeProvider, DependencyInjectorTypeProvider>();
            container.RegisterSingleton<IDependencyInjectorCreator, DependencyInjectorCreator>();
            container.RegisterSingleton<IDependencyInjectorProvider, DependencyInjectorProvider>();
        }
    }
}
