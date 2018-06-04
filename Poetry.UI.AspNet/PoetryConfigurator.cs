using Poetry.UI.AppSupport;
using Poetry.UI.AspNet.DependencyInjectionSupport;
using Poetry.UI.AspNet.FileSupport;
using Poetry.UI.AspNet.PageEditingSupport;
using Poetry.UI.AspNet.RoutingSupport;
using Poetry.UI.ComponentSupport;
using Poetry.UI.ComponentSupport.DependencySupport;
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
        UnityContainer Container { get; }
        public string BasePath { get; private set; } = "Admin";
        List<AssemblyWrapper> Assemblies { get; } = new List<AssemblyWrapper>
        {
            new AssemblyWrapper(typeof(PoetryConfigurator).Assembly),
            new AssemblyWrapper(typeof(FormComponent).Assembly),
            new AssemblyWrapper(typeof(DataTableComponent).Assembly),
            new AssemblyWrapper(typeof(PageEditingComponent).Assembly),
            new AssemblyWrapper(typeof(TranslationComponent).Assembly),
            new AssemblyWrapper(typeof(PortalComponent).Assembly),
        };

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
            Container.RegisterType<T1, T2>();

            return this;
        }

        public PoetryConfigurator InjectInstance<T>(T instance)
        {
            Container.RegisterInstance<T>(instance);

            return this;
        }

        public void Done()
        {
            DependencyResolver.SetResolver(new UnityDependencyResolver(Container));

            Container.RegisterType<IInstantiator, Instantiator>();

            RouteTable.Routes.MapRoute(
                "PoetryPortal",
                BasePath,
                new { controller = "PoetryPortal", action = "Index" },
                namespaces: new string[] { "Poetry.UI.Controllers" }
            );

            Container.RegisterInstance<IBasePathProvider>(new BasePathProvider(BasePath));

            Container.RegisterType<IStyleCreator, StyleCreator>();
            Container.RegisterType<IScriptCreator, ScriptCreator>();
            Container.RegisterType<IControllerActionCreator, ControllerActionCreator>();
            Container.RegisterType<IControllerCreator, ControllerCreator>();
            Container.RegisterType<IComponentControllerTypeProvider, ComponentControllerTypeProvider>();
            Container.RegisterType<IComponentControllerCreator, ComponentControllerCreator>();
            Container.RegisterType<IComponentCreator, ComponentCreator>();
            Container.RegisterType<IComponentDependencyCreator, ComponentDependencyCreator>();
            Container.RegisterType<IComponentDependencySorter, ComponentDependencySorter>();
            Container.RegisterInstance<IComponentTypeProvider>(new ComponentTypeProvider(Assemblies));

            Container.RegisterType<IComponentRepository, ComponentRepository>();

            Container.RegisterType<IObjectIdentifier, ObjectIdentifier>();
            Container.RegisterType<IPropertyExpressionMetaDataProvider, PropertyExpressionMetaDataProvider>();
            Container.RegisterType<IFormFieldCreator, FormFieldCreator>();
            Container.RegisterInstance<IFormTypeProvider>(new FormTypeProvider(Assemblies));
            Container.RegisterType<IFormCreator, FormCreator>();
            Container.RegisterType<IFormFieldProvider, FormFieldProvider>();

            Container.RegisterInstance<IBackendTypeProvider>(new BackendTypeProvider(Assemblies));
            Container.RegisterType<IBackendCreator, BackendCreator>();
            Container.RegisterType<IBackendProvider, BackendProvider>();

            Container.RegisterType<IEmbeddedResourceAssemblyCreator, EmbeddedResourceAssemblyCreator>();
            Container.RegisterType<IEmbeddedResourceAssemblyProvider, EmbeddedResourceAssemblyProvider>();
            Container.RegisterType<IEmbeddedResourcePathMatcher, EmbeddedResourcePathMatcher>();
            Container.RegisterType<IEmbeddedResourceProvider, EmbeddedResourceProvider>();
            Container.RegisterType<EmbeddedResourceVirtualPathViewProvider, EmbeddedResourceVirtualPathViewProvider>();

            HostingEnvironment.RegisterVirtualPathProvider(Container.Resolve<EmbeddedResourceVirtualPathViewProvider>());

            Container.RegisterType<IControllerRouter, ControllerRouter>();

            RouteTable.Routes.Add(Container.Resolve<ControllerRoute>());

            Container.RegisterType<IModeProvider, ModeProvider>();

            Container.RegisterInstance<IAppTypeProvider>(new AppTypeProvider(Assemblies));
            Container.RegisterType<IFileProvider, FileProvider>();
            Container.RegisterType<ITranslationParser, XmlTranslationParser>();
            Container.RegisterType<ITranslationRepositoryCreator, TranslationRepositoryCreator>();
            Container.RegisterType<IScriptCreator, ScriptCreator>();
            Container.RegisterType<IStyleCreator, StyleCreator>();
            Container.RegisterType<IAppCreator, AppCreator>();
            Container.RegisterType<IAppRepository, AppRepository>();
        }
    }
}