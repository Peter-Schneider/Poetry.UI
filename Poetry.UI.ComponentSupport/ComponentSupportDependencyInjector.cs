using Poetry.UI.ComponentSupport.DependencySupport;
using Poetry.UI.ComponentSupport.EmbeddedResourceSupport;
using Poetry.UI.ComponentSupport.InitializerSupport;
using Poetry.UI.ControllerSupport;
using Poetry.UI.DependencyInjectionSupport;
using Poetry.UI.EmbeddedResourceSupport;
using Poetry.UI.ResourceSupport;
using Poetry.UI.RoutingSupport;
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
            container.RegisterSingleton<IPublicComponentEmbeddedResourceProvider, PublicComponentEmbeddedResourceProvider>();
            container.RegisterSingleton<IScriptCreator, ScriptCreator>();
            container.RegisterSingleton<IStyleCreator, StyleCreator>();
            container.RegisterSingleton<IResourceCreator, ResourceCreator>();
            container.RegisterSingleton<IControllerRouter, ControllerRouter>();
            container.RegisterSingleton<IInitializerProvider, InitializerProvider>();
            container.RegisterSingleton<IInitializerTypeProvider, InitializerTypeProvider>();
            container.RegisterSingleton<IInitializerCreator, InitializerCreator>();
        }
    }
}
