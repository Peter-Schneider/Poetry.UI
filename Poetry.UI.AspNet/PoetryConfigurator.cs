using Poetry.UI.AppSupport;
using Poetry.UI.AspNet.FileSupport;
using Poetry.UI.AspNet.MvcSupport;
using Poetry.UI.Core;
using Poetry.UI.EmbeddedResourceSupport;
using Poetry.UI.FormSupport;
using Poetry.UI.Portal;
using Poetry.UI.TranslationSupport;
using System;
using System.Collections.Generic;
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
        string BasePath { get; set; } = "Admin";
        List<Assembly> Assemblies { get; } = new List<Assembly>();
        List<EmbeddedResourceAssembly> EmbeddedResourceAssemblies { get; } = new List<EmbeddedResourceAssembly>();
        EmbeddedResourceAssemblyCreator EmbeddedResourceAssemblyCreator { get; }
        
        public PoetryConfigurator(UnityContainer container) {
            Container = container;
            EmbeddedResourceAssemblyCreator = new EmbeddedResourceAssemblyCreator();
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

        public PoetryConfigurator AddEmbeddedResourceAssembly(string basePath, Assembly assembly)
        {
            if (!Assemblies.Contains(assembly))
            {
                Assemblies.Add(assembly);
            }

            EmbeddedResourceAssemblies.Add(EmbeddedResourceAssemblyCreator.Create(basePath, assembly));

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

            var basePathProvider = (IBasePathProvider)new BasePathProvider(BasePath);

            Container.RegisterInstance(typeof(IBasePathProvider), basePathProvider);

            var assemblies = new List<EmbeddedResourceAssembly>();

            assemblies.Add(EmbeddedResourceAssemblyCreator.Create("Core", Assembly.GetExecutingAssembly()));
            assemblies.Add(EmbeddedResourceAssemblyCreator.Create("Form", typeof(FormComponent).Assembly));
            assemblies.Add(EmbeddedResourceAssemblyCreator.Create("Portal", typeof(PortalComponent).Assembly));

            assemblies.AddRange(EmbeddedResourceAssemblies);

            var embeddedResourceProvider = new EmbeddedResourceProvider(new EmbeddedResourcePathMatcher(), assemblies.ToArray());

            var vpp = new EmbeddedResourceVirtualPathProvider(basePathProvider, embeddedResourceProvider);

            Container.RegisterInstance(typeof(EmbeddedResourceVirtualPathProvider), vpp);

            RouteTable.Routes.Add(new EmbeddedResourceRoute(vpp));
            RouteTable.Routes.Add(new MvcRoute());
            RouteTable.Routes.RouteExistingFiles = true;

            HostingEnvironment.RegisterVirtualPathProvider(vpp);

            Container.RegisterInstance(typeof(IAppRepository), new AppRepository(new AppCreator(new TranslationRepositoryCreator(new FileProvider(), new XmlTranslationParser())).Create(Assemblies)));
        }
    }
}