using Poetry.UI.EmbeddedResourceSupport;
using Poetry.UI.RoutingSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Poetry.UI.AspNet.EmbeddedResourceSupport
{
    public class EmbeddedResourceModule : IHttpModule
    {
        IBasePathProvider BasePathProvider { get; set; }
        IEmbeddedResourceProvider EmbeddedResourceProvider { get; set; }
        string Prefix { get; set; }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += Context_BeginRequest;
        }

        void Context_BeginRequest(object sender, EventArgs e)
        {
            if (BasePathProvider == null)
            {
                BasePathProvider = DependencyResolver.Current.GetService<IBasePathProvider>();
                Prefix = $"/{BasePathProvider.BasePath}/";
            }

            if (EmbeddedResourceProvider == null)
            {
                EmbeddedResourceProvider = DependencyResolver.Current.GetService<IEmbeddedResourceProvider>();
            }

            var context = ((HttpApplication)sender).Context;
            var path = context.Request.Path;

            if (!path.StartsWith(Prefix, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            path = path.Substring(Prefix.Length);

            var file = EmbeddedResourceProvider.GetFile(path);

            if(file == null)
            {
                return;
            }

            context.RemapHandler(new EmbeddedResourceHandler());
        }

        public void Dispose() { }
    }
}
