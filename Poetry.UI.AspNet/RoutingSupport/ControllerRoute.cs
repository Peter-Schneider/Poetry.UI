using Poetry.UI.RoutingSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Poetry.UI.AspNet.RoutingSupport
{
    public class ControllerRoute : RouteBase
    {
        ControllerRouter ControllerRouter { get; }

        public ControllerRoute(ControllerRouter controllerRouter)
        {
            ControllerRouter = controllerRouter;
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var result = ControllerRouter.Route(httpContext.Request.Url.LocalPath);

            if (result == null)
            {
                return null;
            }

            var data = new RouteData(this, new MvcRouteHandler());
            data.Values.Add("controller", "ControllerRouting");
            data.Values.Add("action", "Index");
            data.DataTokens.Add("Namespaces", new string[] { "Poetry.UI.AspNet.RoutingSupport" });
            data.DataTokens.Add("UseNamespaceFallback", false);
            data.DataTokens.Add("PoetryRoutingResult", result);
            return data;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}
