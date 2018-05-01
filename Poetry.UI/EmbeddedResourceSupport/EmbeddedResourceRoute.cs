using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Poetry.UI.EmbeddedResourceSupport
{
    public class EmbeddedResourceRoute : RouteBase
    {
        EmbeddedResourceVirtualPathProvider EmbeddedResourceVirtualPathProvider { get; }

        public EmbeddedResourceRoute(EmbeddedResourceVirtualPathProvider embeddedResourceVirtualPathProvider)
        {
            EmbeddedResourceVirtualPathProvider = embeddedResourceVirtualPathProvider;
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (!EmbeddedResourceVirtualPathProvider.FileExists(httpContext.Request.Url.LocalPath))
            {
                return null;
            }

            var result = new RouteData(this, new MvcRouteHandler());
            result.Values.Add("controller", "PoetryPortal");
            result.Values.Add("action", "StaticFile");
            result.DataTokens.Add("Namespaces", new string[] { "Poetry.UI.Controllers" });
            result.DataTokens.Add("UseNamespaceFallback", false);
            return result;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}
