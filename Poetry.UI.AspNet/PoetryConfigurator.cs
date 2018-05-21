using Poetry.UI.AppSupport;
using Poetry.UI.AspNet.FileSupport;
using Poetry.UI.AspNet.RoutingSupport;
using Poetry.UI.ComponentSupport;
using Poetry.UI.ControllerSupport;
using Poetry.UI.EmbeddedResourceSupport;
using Poetry.UI.FormSupport;
using Poetry.UI.FormSupport.FormFieldSupport;
using Poetry.UI.PortalSupport;
using Poetry.UI.RoutingSupport;
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
        List<Assembly> Assemblies { get; } = new List<Assembly>();
        List<Component> Components { get; } = new List<Component>();
        
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

            RouteTable.Routes.MapRoute(
                "PoetryPortal",
                BasePath,
                new { controller = "PoetryPortal", action = "Index" },
                namespaces: new string[] { "Poetry.UI.Controllers" }
            );

            var basePathProvider = (IBasePathProvider)this;

            Container.RegisterInstance(typeof(IBasePathProvider), basePathProvider);

            var componentCreator = new ComponentCreator(new ComponentControllerCreator(new ComponentControllerTypeProvider(), new ControllerCreator(new ControllerActionCreator())));

            Components.Add(componentCreator.Create(typeof(PortalComponent)));
            Components.Add(componentCreator.Create(typeof(FormComponent)));
            Container.RegisterInstance(typeof(IFormFieldProvider), new FormFieldProvider(new FormCreator(new FormFieldCreator()).Create(new FormTypeProvider().GetTypes(Assemblies.ToArray()).ToArray()).ToDictionary(f => f.Id, f => f.Fields)));
            Components.Add(componentCreator.Create(typeof(DataTableComponent)));

            var embeddedResourceAssemblyCreator = new EmbeddedResourceAssemblyCreator();
            var embeddedResourceAssemblies = new List<EmbeddedResourceAssembly>();

            embeddedResourceAssemblies.Add(embeddedResourceAssemblyCreator.Create("Core", Assembly.GetExecutingAssembly()));
            embeddedResourceAssemblies.AddRange(Components.Select(c => embeddedResourceAssemblyCreator.Create(c.Id, c.Assembly)));

            var embeddedResourceProvider = new EmbeddedResourceProvider(new EmbeddedResourcePathMatcher(), embeddedResourceAssemblies.ToArray());

            var vpp = new EmbeddedResourceVirtualPathProvider(basePathProvider, embeddedResourceProvider);

            Container.RegisterInstance(typeof(EmbeddedResourceVirtualPathProvider), vpp);

            RouteTable.Routes.Add(new EmbeddedResourceRoute(vpp));
            RouteTable.Routes.Add(new ControllerRoute(new ControllerRouter(basePathProvider, Components.ToArray())));
            RouteTable.Routes.RouteExistingFiles = true;

            HostingEnvironment.RegisterVirtualPathProvider(vpp);

            Container.RegisterInstance(typeof(IAppRepository), new AppRepository(new AppCreator(new TranslationRepositoryCreator(new FileProvider(), new XmlTranslationParser())).Create(Assemblies)));
        }
    }
}