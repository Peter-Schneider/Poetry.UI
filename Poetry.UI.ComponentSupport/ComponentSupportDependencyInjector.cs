using Poetry.UI.ComponentSupport.DependencySupport;
using Poetry.UI.ControllerSupport;
using Poetry.UI.DependencyInjectionSupport;
using Poetry.UI.EmbeddedResourceSupport;
using Poetry.UI.ScriptSupport;
using Poetry.UI.StyleSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.ComponentSupport
{
    public class ComponentSupportDependencyInjector : IDependencyInjector
    {
        public void InjectDependencies(IContainer container)
        {
            container.RegisterSingleton<IComponentControllerTypeProvider, ComponentControllerTypeProvider>();
            container.RegisterSingleton<IComponentControllerCreator, ComponentControllerCreator>();
            container.RegisterSingleton<IComponentTypeProvider, ComponentTypeProvider>();
            container.RegisterSingleton<IComponentCreator, ComponentCreator>();
            container.RegisterSingleton<IComponentDependencyCreator, ComponentDependencyCreator>();
            container.RegisterSingleton<IComponentDependencySorter, ComponentDependencySorter>();
            container.RegisterSingleton<IComponentRepository, ComponentRepository>();
            container.RegisterSingleton<IEmbeddedResourceAssemblyProvider, EmbeddedResourceAssemblyProvider>();
            container.RegisterSingleton<IScriptCreator, ScriptCreator>();
            container.RegisterSingleton<IStyleCreator, StyleCreator>();
        }
    }
}
