using Poetry.UI.AppSupport.EmbeddedResourceSupport;
using Poetry.UI.ComponentSupport.EmbeddedResourceSupport;
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
        IPublicAppEmbeddedResourceProvider PublicAppEmbeddedResourceProvider { get; set; }
        IPublicComponentEmbeddedResourceProvider PublicComponentEmbeddedResourceProvider { get; set; }
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

            if (PublicAppEmbeddedResourceProvider == null)
            {
                PublicAppEmbeddedResourceProvider = DependencyResolver.Current.GetService<IPublicAppEmbeddedResourceProvider>();
            }

            if (PublicComponentEmbeddedResourceProvider == null)
            {
                PublicComponentEmbeddedResourceProvider = DependencyResolver.Current.GetService<IPublicComponentEmbeddedResourceProvider>();
            }

            var context = ((HttpApplication)sender).Context;
            var path = context.Request.Path;

            if (!path.StartsWith(Prefix, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            path = path.Substring(Prefix.Length);

            var file = PublicAppEmbeddedResourceProvider.GetFile(path) ?? PublicComponentEmbeddedResourceProvider.GetFile(path);

            if(file == null)
            {
                return;
            }

            context.RemapHandler(new EmbeddedResourceHandler());
        }

        public void Dispose() { }
    }
}
