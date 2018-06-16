using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.DependencyInjectionSupport
{
    public class DependencyInjectionSupportDependencyInjector : IDependencyInjector
    {
        public void InjectDependencies(IContainer container)
        {
            container.RegisterType<IDependencyInjectorTypeProvider, DependencyInjectorTypeProvider>();
            container.RegisterType<IDependencyInjectorCreator, DependencyInjectorCreator>();
            container.RegisterType<IDependencyInjectorProvider, DependencyInjectorProvider>();
        }
    }
}
