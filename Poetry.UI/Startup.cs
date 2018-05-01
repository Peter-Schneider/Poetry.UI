using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web.Hosting;
using Poetry.UI.VirtualPathProviderSupport;
using Poetry.UI.MvcSupport;
using Poetry.UI.AppSupport;
using System.Reflection;

namespace Poetry.UI
{
    public static class Startup
    {
        public static void AddPoetryUI(this HttpApplication application, Action<Type, object> registerSingletonInDependencyResolver, IEnumerable<Assembly> assemblies, string basePath = "Poetry")
        {
            //RouteTable.Routes.MapRoute(
            //    "PoetryPortal",
            //    basePath,
            //    new { controller = "Portal", action = "Index" },
            //    namespaces: new string[] { "Poetry.UI.Controllers" }
            //);

            RouteTable.Routes.MapRoute(
                "PoetryStaticFiles",
                basePath + "/{*virtualPath}",
                new { controller = "Portal", action = "StaticFile" },
                namespaces: new string[] { "Poetry.UI.Controllers" }
            );

            registerSingletonInDependencyResolver(typeof(IBasePathProvider), new BasePathProvider(basePath));
            registerSingletonInDependencyResolver(typeof(IAppRepository), new AppRepository(new AppCreator().Create(assemblies)));

            HostingEnvironment.RegisterVirtualPathProvider(new EmbeddedResourceVirtualPathProvider(DependencyResolver.Current.GetService<IBasePathProvider>()));
        }
    }
}
