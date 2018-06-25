using Microsoft.Extensions.Logging;
using Poetry.UI.AppSupport;
using Poetry.UI.AspNet;
using Poetry.UI.AspNet.DependencyInjectionSupport;
using Poetry.UI.AspNet.LoggerSupport;
using Poetry.UI.AspNet.PageEditingSupport;
using Poetry.UI.AspNet.RoutingSupport;
using Poetry.UI.BladeSupport;
using Poetry.UI.ComponentSupport;
using Poetry.UI.ComponentSupport.DependencySupport;
using Poetry.UI.ComponentSupport.InitializerSupport;
using Poetry.UI.ContextMenu;
using Poetry.UI.ControllerSupport;
using Poetry.UI.DataTableSupport.BackendSupport;
using Poetry.UI.DependencyInjectionSupport;
using Poetry.UI.EmbeddedResourceSupport;
using Poetry.UI.FormSupport;
using Poetry.UI.FormSupport.FormFieldSupport;
using Poetry.UI.PageEditingSupport;
using Poetry.UI.PortalSupport;
using Poetry.UI.ReflectionSupport;
using Poetry.UI.RoutingSupport;
using Poetry.UI.ScriptSupport;
using Poetry.UI.StyleSupport;
using Poetry.UI.TableSupport;
using Poetry.UI.TranslationSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using Unity;
using Unity.AspNet.Mvc;

namespace Poetry.UI
{
    public class PoetryConfigurator
    {
        IUnityContainer Container { get; }
        string BasePath { get; set; } = "Admin";
        List<AssemblyWrapper> Assemblies { get; } = new List<AssemblyWrapper>
        {
            new AssemblyWrapper(typeof(PoetryConfigurator).Assembly),
            new AssemblyWrapper(typeof(PortalComponent).Assembly),
            new AssemblyWrapper(typeof(BladeComponent).Assembly),
            new AssemblyWrapper(typeof(TranslationComponent).Assembly),
            new AssemblyWrapper(typeof(DataTableComponent).Assembly),
            new AssemblyWrapper(typeof(FormComponent).Assembly),
            new AssemblyWrapper(typeof(ContextMenuComponent).Assembly),
            new AssemblyWrapper(typeof(PageEditingComponent).Assembly),
        };
        List<Action<IContainer>> ContainerOverrides { get; } = new List<Action<IContainer>>();

        public PoetryConfigurator(UnityContainer container) {
            Container = container;
        }

        public PoetryConfigurator WithBasePath(string basePath)
        {
            BasePath = basePath.Trim('/');
            return this;
        }

        public PoetryConfigurator AddAssembly(Assembly assembly)
        {
            Assemblies.Add(new AssemblyWrapper(assembly));
            return this;
        }

        public PoetryConfigurator InjectType<T1, T2>() where T1 : class where T2 : class, T1
        {
            ContainerOverrides.Add(c => c.RegisterType<T1, T2>());

            return this;
        }

        public PoetryConfigurator InjectSingleton<T1, T2>() where T1 : class where T2 : class, T1
        {
            ContainerOverrides.Add(c => c.RegisterSingleton<T1, T2>());

            return this;
        }

        public PoetryConfigurator InjectSingleton<T>(T instance) where T : class
        {
            ContainerOverrides.Add(c => c.RegisterSingleton(instance));

            return this;
        }

        public void Done()
        {
            var poetryContainer = new Container(Container);

            poetryContainer.RegisterType(typeof(ILogger<>), typeof(DefaultLogger<>));
            poetryContainer.RegisterType<IInstantiator, Instantiator>();
            poetryContainer.RegisterSingleton<IBasePathProvider>(new BasePathProvider(BasePath));
            poetryContainer.RegisterSingleton<IAssemblyProvider>(new AssemblyProvider(Assemblies));

            new AspNetSpecificDependencyInjector().InjectDependencies(poetryContainer);

            new ComponentSupportDependencyInjector().InjectDependencies(poetryContainer);
            new DependencyInjectionSupportDependencyInjector().InjectDependencies(poetryContainer);
            new ControllerSupportDependencyInjector().InjectDependencies(poetryContainer);
            new EmbeddedResourceSupportDependencyInjector().InjectDependencies(poetryContainer);
            new AppSupportDependencyInjector().InjectDependencies(poetryContainer);

            foreach(var injector in Container.Resolve<IDependencyInjectorProvider>().GetAll())
            {
                injector.InjectDependencies(poetryContainer);
            }

            foreach(var containerOverride in ContainerOverrides)
            {
                containerOverride(poetryContainer);
            }

            DependencyResolver.SetResolver(new UnityDependencyResolver(Container));
            RouteTable.Routes.MapRoute(
                "PoetryPortal",
                BasePath,
                new { controller = "PoetryPortal", action = "Index" },
                namespaces: new string[] { "Poetry.UI.Controllers" }
            );
            HostingEnvironment.RegisterVirtualPathProvider(Container.Resolve<EmbeddedResourceVirtualPathViewProvider>());
            RouteTable.Routes.Add(Container.Resolve<ControllerRoute>());

            foreach(var initializer in Container.Resolve<IInitializerProvider>().GetAll())
            {
                initializer.Initialize();
            }
        }
    }
}