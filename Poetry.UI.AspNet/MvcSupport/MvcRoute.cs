using Poetry.UI.RoutingSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Poetry.UI.AspNet.MvcSupport
{
    public class MvcRoute : RouteBase
    {
        ControllerRouter ControllerRouter { get; }

        public MvcRoute(ControllerRouter controllerRouter)
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
            data.Values.Add("controller", "Mvc");
            data.Values.Add("action", "Index");
            data.DataTokens.Add("Namespaces", new string[] { "Poetry.UI.AspNet.MvcSupport" });
            data.DataTokens.Add("UseNamespaceFallback", false);
            return data;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}
