using Poetry.UI.AppSupport;
using Poetry.UI.AspNet.DependencyInjectionSupport;
using Poetry.UI.AspNet.FileSupport;
using Poetry.UI.AspNet.PageEditingSupport;
using Poetry.UI.AspNet.RoutingSupport;
using Poetry.UI.ComponentSupport;
using Poetry.UI.ComponentSupport.DependencySupport;
using Poetry.UI.ControllerSupport;
using Poetry.UI.DependencyInjectionSupport;
using Poetry.UI.EmbeddedResourceSupport;
using Poetry.UI.FormSupport;
using Poetry.UI.FormSupport.FormFieldSupport;
using Poetry.UI.PageEditingSupport;
using Poetry.UI.PortalSupport;
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
    public class PoetryConfigurator : IBasePathProvider
    {
        UnityContainer Container { get; }
        public string BasePath { get; private set; } = "Admin";
        List<Assembly> Assemblies { get; } = new List<Assembly>
        {
            typeof(PoetryConfigurator).Assembly,
            typeof(FormComponent).Assembly,
            typeof(DataTableComponent).Assembly,
            typeof(PageEditingComponent).Assembly,
            typeof(TranslationComponent).Assembly,
            typeof(PortalComponent).Assembly,
        };
        
        public PoetryConfigurator(UnityContainer container) {
            Container = container;
        }

        public PoetryConfigurator WithBasePath(string basePath)
        {
            BasePath = basePath;
            return this;
        }

        public PoetryConfigurator AddAssembly(Assembly assembly)
        {
            Assemblies.Add(assembly);
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

            Container.RegisterInstance<IBasePathProvider>(this);

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
            Container.RegisterInstance<IFormFieldProvider>(new FormFieldProvider(new FormCreator(new FormFieldCreator()).Create(new FormTypeProvider().GetTypes(Assemblies.ToArray()).ToArray()).ToDictionary(f => f.Id, f => f.Fields)));
            Container.RegisterInstance<DataTableSupport.BackendSupport.IBackendProvider>(new DataTableSupport.BackendSupport.BackendProvider(new DataTableSupport.BackendSupport.BackendCreator(new Instantiator()).Create(new DataTableSupport.BackendSupport.BackendTypeProvider().GetTypes(Assemblies))));

            Container.RegisterType<IEmbeddedResourceAssemblyCreator, EmbeddedResourceAssemblyCreator>();
            Container.RegisterType<IEmbeddedResourceAssemblyProvider, EmbeddedResourceAssemblyProvider>();
            Container.RegisterType<IEmbeddedResourcePathMatcher, EmbeddedResourcePathMatcher>();
            Container.RegisterType<IEmbeddedResourceProvider, EmbeddedResourceProvider>();
            Container.RegisterType<EmbeddedResourceVirtualPathViewProvider, EmbeddedResourceVirtualPathViewProvider>();

            HostingEnvironment.RegisterVirtualPathProvider(Container.Resolve<EmbeddedResourceVirtualPathViewProvider>());

            Container.RegisterType<IControllerRouter, ControllerRouter>();

            RouteTable.Routes.Add(Container.Resolve<ControllerRoute>());

            Container.RegisterType<IModeProvider, ModeProvider>();

            Container.RegisterInstance<IAppRepository>(new AppRepository(new AppCreator(new TranslationRepositoryCreator(new FileProvider(), new XmlTranslationParser()), new ScriptCreator(), new StyleCreator()).Create(Assemblies)));
        }
    }
}