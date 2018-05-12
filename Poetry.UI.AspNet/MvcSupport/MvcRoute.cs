using Poetry.UI.MvcSupport;
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
            if(ControllerRouter.Route(httpContext.Request.Url.LocalPath) == null)
            {
                return null;
            }

            var result = new RouteData(this, new MvcRouteHandler());
            result.Values.Add("controller", "Mvc");
            result.Values.Add("action", "Index");
            result.DataTokens.Add("Namespaces", new string[] { "Poetry.UI.AspNet.MvcSupport" });
            result.DataTokens.Add("UseNamespaceFallback", false);
            return result;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}
