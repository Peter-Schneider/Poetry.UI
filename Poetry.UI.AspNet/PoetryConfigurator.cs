using Microsoft.Extensions.Logging;
using Poetry.UI.AppSupport;
using Poetry.UI.AspNet.DependencyInjectionSupport;
using Poetry.UI.AspNet.FileSupport;
using Poetry.UI.AspNet.LoggerSupport;
using Poetry.UI.AspNet.PageEditingSupport;
using Poetry.UI.AspNet.RoutingSupport;
using Poetry.UI.ComponentSupport;
using Poetry.UI.ComponentSupport.DependencySupport;
using Poetry.UI.ContextMenu;
using Poetry.UI.ControllerSupport;
using Poetry.UI.DataTableSupport.BackendSupport;
using Poetry.UI.DependencyInjectionSupport;
using Poetry.UI.EmbeddedResourceSupport;
using Poetry.UI.FileSupport;
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
        public string BasePath { get; private set; } = "Admin";
        List<AssemblyWrapper> Assemblies { get; } = new List<AssemblyWrapper>
        {
            new AssemblyWrapper(typeof(PoetryConfigurator).Assembly),
            new AssemblyWrapper(typeof(ContextMenuComponent).Assembly),
            new AssemblyWrapper(typeof(FormComponent).Assembly),
            new AssemblyWrapper(typeof(DataTableComponent).Assembly),
            new AssemblyWrapper(typeof(PageEditingComponent).Assembly),
            new AssemblyWrapper(typeof(TranslationComponent).Assembly),
            new AssemblyWrapper(typeof(PortalComponent).Assembly),
        };
        List<Action<IUnityContainer>> ContainerOverrides { get; } = new List<Action<IUnityContainer>>();

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

        public PoetryConfigurator InjectType<T1, T2>() where T2 : T1
        {
            ContainerOverrides.Add(c => c.RegisterType<T1, T2>());

            return this;
        }

        public PoetryConfigurator InjectSingleton<T1, T2>() where T2 : T1
        {
            ContainerOverrides.Add(c => c.RegisterSingleton<T1, T2>());

            return this;
        }

        public PoetryConfigurator InjectInstance<T>(T instance)
        {
            ContainerOverrides.Add(c => c.RegisterInstance(instance));

            return this;
        }

        public void Done()
        {
            DependencyResolver.SetResolver(new UnityDependencyResolver(Container));

            Container.RegisterType(typeof(ILogger<>), typeof(DefaultLogger<>));
            Container.RegisterType<IInstantiator, Instantiator>();

            RouteTable.Routes.MapRoute(
                "PoetryPortal",
                BasePath,
                new { controller = "PoetryPortal", action = "Index" },
                namespaces: new string[] { "Poetry.UI.Controllers" }
            );

            Container.RegisterInstance<IBasePathProvider>(new BasePathProvider(BasePath));

            var poetryContainer = new Container(Container);

            new ScriptSupportDependencyInjector().InjectDependencies(poetryContainer);
            new StyleSupportDependencyInjector().InjectDependencies(poetryContainer);
            new ComponentSupportDependencyInjector().InjectDependencies(poetryContainer);
            new DependencyInjectionSupportDependencyInjector().InjectDependencies(poetryContainer);
            new ControllerSupportDependencyInjector().InjectDependencies(poetryContainer);
            new EmbeddedResourceSupportDependencyInjector().InjectDependencies(poetryContainer);
            new RoutingSupportDependencyInjector().InjectDependencies(poetryContainer);
            new AppSupportDependencyInjector().InjectDependencies(poetryContainer);

            Container.RegisterInstance<IDependencyInjectorTypeProvider>(new DependencyInjectorTypeProvider(Assemblies));
            
            Container.RegisterInstance<IComponentTypeProvider>(new ComponentTypeProvider(Assemblies));

            foreach(var injector in Container.Resolve<IDependencyInjectorProvider>().GetAll())
            {
                injector.InjectDependencies(poetryContainer);
            }

            Container.RegisterInstance<IFormTypeProvider>(new FormTypeProvider(Assemblies));

            Container.RegisterInstance<IBackendTypeProvider>(new BackendTypeProvider(Assemblies));

            Container.RegisterType<IModeProvider, ModeProvider>();

            HostingEnvironment.RegisterVirtualPathProvider(Container.Resolve<EmbeddedResourceVirtualPathViewProvider>());
            RouteTable.Routes.Add(Container.Resolve<ControllerRoute>());

            Container.RegisterInstance<IAppTypeProvider>(new AppTypeProvider(Assemblies));
            Container.RegisterType<IFileProvider, FileProvider>();

            foreach(var containerOverride in ContainerOverrides)
            {
                containerOverride(Container);
            }
        }
    }
}