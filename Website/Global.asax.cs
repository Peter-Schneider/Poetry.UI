using Poetry.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Website.DomainObjects;

namespace Website
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.MapHttpAttributeRoutes();
            GlobalConfiguration.Configuration.EnsureInitialized();

            this.AddPoetryUI().AddAssembly(typeof(DomainObjectsComponent).Assembly).Done();

            RouteTable.Routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );

            RouteTable.Routes.MapRoute(
                name: "ViewCategory",
                url: "{category}",
                defaults: new { controller = "Category", action = "Index" }
            );

            RouteTable.Routes.MapRoute(
                name: "ViewProduct",
                url: "{category}/{product}",
                defaults: new { controller = "Product", action = "Index" }
            );
        }
    }
}
