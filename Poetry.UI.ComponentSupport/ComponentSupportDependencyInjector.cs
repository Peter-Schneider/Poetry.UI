using Poetry.UI.ComponentSupport.DependencySupport;
using Poetry.UI.DependencyInjectionSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.ComponentSupport
{
    public class ComponentSupportDependencyInjector : IDependencyInjector
    {
        public void InjectDependencies(IContainer container)
        {
            container.RegisterType<IComponentControllerTypeProvider, ComponentControllerTypeProvider>();
            container.RegisterType<IComponentControllerCreator, ComponentControllerCreator>();
            container.RegisterType<IComponentCreator, ComponentCreator>();
            container.RegisterType<IComponentDependencyCreator, ComponentDependencyCreator>();
            container.RegisterType<IComponentDependencySorter, ComponentDependencySorter>();
            container.RegisterType<IComponentRepository, ComponentRepository>();
        }
    }
}
